using System.Web;

using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database.Processors;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Models;
using Bank.ExchangeService.Repositories;

namespace Bank.ExchangeService.BackgroundServices;

public class StockBackgroundService(
    IEnumerable<IRealtimeProcessor> realtimeProcessors,
    IHttpClientFactory              httpClientFactory,
    ISecurityRepository             securityRepository,
    ILogger<StockBackgroundService> logger
)

{
    private readonly ILogger<StockBackgroundService> m_Logger             = logger;
    private          Timer                           m_Timer              = null!;
    private readonly IEnumerable<IRealtimeProcessor> m_RealtimeProcessors = realtimeProcessors;
    private readonly IHttpClientFactory              m_HttpClientFactory  = httpClientFactory;
    private readonly ISecurityRepository             m_SecurityRepository = securityRepository;
    private          Dictionary<string, Security>    m_StockDictionary    = null!;
    private          string[]                        m_SymbolsArray       = [];
    private const    int                             c_ReadAmount         = 1000;

    public async Task OnApplicationStarted(CancellationToken cancellationToken)
    {
        m_StockDictionary = (await m_SecurityRepository.FindAll(SecurityType.Stock)).ToDictionary(stock => stock.Ticker, stock => stock);

        m_SymbolsArray = m_StockDictionary.Values.Select((value, index) => new { Index = index, Value = value })
                                          .GroupBy(pair => pair.Index / c_ReadAmount)
                                          .Select(group => string.Join(",", group.Select(pair => pair.Value.Ticker)
                                                                                 .ToList()))
                                          .ToArray();

        m_Timer = new Timer(_ => FetchQuotes()
                            .Wait(cancellationToken), null, TimeSpan.FromMinutes(Configuration.Security.Global.LatestTimeFrameInMinutes),
                            TimeSpan.FromMinutes(Configuration.Security.Global.LatestTimeFrameInMinutes));
    }

    private async Task FetchQuotes()
    {
        var httpClient = m_HttpClientFactory.CreateClient();

        var quotes = new List<Quote>();
        var query  = HttpUtility.ParseQueryString(string.Empty);

        m_Logger.LogInformation("Realtime | Quotes | Stock | Start");
        
        foreach (var symbols in m_SymbolsArray)
        {
            query["symbols"] = symbols;

            var (apiKey, apiSecret) = Configuration.Security.Keys.AlpacaApiKeyAndSecret;

            var request = new HttpRequestMessage
                          {
                              Method     = HttpMethod.Get,
                              RequestUri = new Uri($"{Configuration.Security.Stock.GetLatest}?{query}"),
                              Headers =
                              {
                                  { "accept", "application/json" },
                                  { "APCA-API-KEY-ID", apiKey },
                                  { "APCA-API-SECRET-KEY", apiSecret },
                              }
                          };

            var response = await httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                return;

            var body = await response.Content.ReadFromJsonAsync<Dictionary<string, FetchStockSnapshotResponse>>();

            if (body is null)
                return;

            foreach (var pair in body)
            {
                if (pair.Value is not { DailyBar: not null, LatestQuote: not null, MinuteBar: not null })
                    continue;

                if (!m_StockDictionary.TryGetValue(pair.Key, out var security))
                    continue;

                var quote = pair.Value.ToQuote(security);
                quotes.Add(quote);
            }
        }

        await Task.WhenAll(m_RealtimeProcessors.Select(realtimeProcessor => realtimeProcessor.ProcessStockQuotes(quotes))
                                               .ToList());
        
        m_Logger.LogInformation("Realtime | Quotes | Stock | Complete | Count: {Count}", quotes.Count);
    }

    public Task OnApplicationStopped(CancellationToken cancellationToken)
    {
        m_Timer.Cancel();

        return Task.CompletedTask;
    }
}

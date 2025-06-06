using System.Web;

using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Responses;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database.Processors;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Models;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

namespace Bank.ExchangeService.BackgroundServices;

public class StockBackgroundService(
    IEnumerable<IRealtimeProcessor> realtimeProcessors,
    IHttpClientFactory              httpClientFactory,
    IUserServiceHttpClient          userServiceHttpClient,
    ISecurityRepository             securityRepository
)

{
    private          Timer                           m_Timer              = null!;
    private readonly IEnumerable<IRealtimeProcessor> m_RealtimeProcessors = realtimeProcessors;
    private readonly IHttpClientFactory              m_HttpClientFactory  = httpClientFactory;
    private readonly ISecurityRepository             m_SecurityRepository = securityRepository;
    private          Dictionary<string, Security>    m_StockDictionary    = null!;
    private          List<string>                    m_StockSymbols       = [];

    public async Task OnApplicationStarted(CancellationToken cancellationToken)
    {
        // init
        m_StockDictionary = (await m_SecurityRepository.FindAll(SecurityType.Stock)).ToDictionary(stock => stock.Ticker, stock => stock);

        m_StockSymbols = m_StockDictionary.Values.Select(stock => stock.Ticker)
                                          .ToList();

        m_Timer = new Timer(_ => FetchQuotes()
                            .Wait(cancellationToken), null, TimeSpan.FromMinutes(Configuration.Security.Global.LatestTimeFrameInMinutes),
                            TimeSpan.FromMinutes(Configuration.Security.Global.LatestTimeFrameInMinutes));
    }

    private async Task FetchQuotes()
    {
        //fetch
        var httpClient = m_HttpClientFactory.CreateClient();

        var quotes  = new List<Quote>();
        var query   = HttpUtility.ParseQueryString(string.Empty);
        var batches = m_StockSymbols.Chunk(1000);

        foreach (var batch in batches)
        {
            query["symbols"] = string.Join(",", batch);

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

                var quote = pair.Value.ToQuote(m_StockDictionary[pair.Key]);
                quotes.Add(quote);
            }
        }

        await Task.WhenAll(m_RealtimeProcessors.Select(realtimeProcessor => realtimeProcessor.ProcessStockQuotes(quotes))
                                               .ToList());
    }

    public Task OnApplicationStopped(CancellationToken cancellationToken)
    {
        m_Timer.Cancel();

        return Task.CompletedTask;
    }
}

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

public class OptionBackgroundService(
    IEnumerable<IRealtimeProcessor>  realtimeProcessors,
    IHttpClientFactory               httpClientFactory,
    ISecurityRepository              securityRepository,
    ILogger<OptionBackgroundService> logger
)
{
    private readonly ILogger<OptionBackgroundService> m_Logger             = logger;
    private          Timer                            m_Timer              = null!;
    private readonly IEnumerable<IRealtimeProcessor>  m_RealtimeProcessors = realtimeProcessors;
    private readonly IHttpClientFactory               m_HttpClientFactory  = httpClientFactory;
    private readonly ISecurityRepository              m_SecurityRepository = securityRepository;
    private          string[]                         m_SymbolsArray       = [];
    private          Dictionary<string, Security>     m_OptionsDictionary  = [];
    private const    int                              c_ReadAmount         = 100;

    public async Task OnApplicationStarted(CancellationToken cancellationToken)
    {
        m_OptionsDictionary = (await m_SecurityRepository.FindAll(SecurityType.Option)).ToDictionary(option => option.Ticker, option => option);

        m_SymbolsArray = m_OptionsDictionary.Values.Select((value, index) => new { Index = index, Value = value })
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

        string? nextPage = null;
        var     quotes   = new List<Quote>();
        var     query    = HttpUtility.ParseQueryString(string.Empty);
        query["feed"]  = "indicative";
        query["limit"] = "1000";

        m_Logger.LogInformation("Realtime | Quotes | Options | Start");

        foreach (var symbols in m_SymbolsArray)
        {
            var (apiKey, apiSecret) = Configuration.Security.Keys.AlpacaApiKeyAndSecret;

            query["symbols"] = symbols;

            do
            {
                if (!string.IsNullOrEmpty(nextPage))
                    query["page_token"] = nextPage;
                else
                    query.Remove("page_token");

                var request = new HttpRequestMessage
                              {
                                  Method     = HttpMethod.Get,
                                  RequestUri = new Uri($"{Configuration.Security.Option.OptionChainApi}?{query}"),
                                  Headers =
                                  {
                                      { "accept", "application/json" },
                                      { "APCA-API-KEY-ID", apiKey },
                                      { "APCA-API-SECRET-KEY", apiSecret },
                                  },
                              };

                using var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Failed to fetch options chain: {response.StatusCode}");
                    continue;
                }

                var body = await response.Content.ReadFromJsonAsync<FetchOptionsResponse>();

                if (body == null)
                    continue;

                foreach (var pair in body.Snapshots)
                {
                    if (pair.Value is not { DailyBar: not null, LatestQuote: not null } || !m_OptionsDictionary.TryGetValue(pair.Key, out var security))
                        continue;
                    
                    var quote = pair.Value.ToQuote(security);
                    quotes.Add(quote);
                }

                nextPage = body.NextPage;
            } while (!string.IsNullOrEmpty(nextPage));
        }

        await Task.WhenAll(m_RealtimeProcessors.Select(realtimeProcessor => realtimeProcessor.ProcessOptionQuotes(quotes))
                                               .ToList());
        
        m_Logger.LogInformation("Realtime | Quotes | Options | Complete | Count: {Count}", quotes.Count);
    }

    public Task OnApplicationStopped(CancellationToken _)
    {
        m_Timer.Cancel();

        return Task.CompletedTask;
    }
}

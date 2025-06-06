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

public class OptionBackgroundService(IEnumerable<IRealtimeProcessor> realtimeProcessors, IHttpClientFactory httpClientFactory, ISecurityRepository securityRepository)
{
    private          Timer                           m_Timer              = null!;
    private readonly IEnumerable<IRealtimeProcessor> m_RealtimeProcessors = realtimeProcessors;
    private readonly IHttpClientFactory              m_HttpClientFactory  = httpClientFactory;
    private readonly ISecurityRepository             m_SecurityRepository = securityRepository;
    private          List<Security>                  m_Options            = [];

    public async Task OnApplicationStarted(CancellationToken cancellationToken)
    {
        m_Options = (await m_SecurityRepository.FindAll(SecurityType.Option)).ToList();

        m_Timer = new Timer(_ => FetchQuotes()
                            .Wait(cancellationToken), null, TimeSpan.FromMinutes(Configuration.Security.Global.LatestTimeFrameInMinutes),
                            TimeSpan.FromMinutes(Configuration.Security.Global.LatestTimeFrameInMinutes));
    }

    private async Task FetchQuotes()
    {
        var       httpClient = m_HttpClientFactory.CreateClient();
        const int readAmount = 100;
        string?   nextPage   = null;
        var       quotes     = new List<Quote>();
        var       query      = HttpUtility.ParseQueryString(string.Empty);
        query["feed"]  = "indicative";
        query["limit"] = "1000";

        for (int i = 1; i * readAmount < m_Options.Count + readAmount; i++)
        {
            var (apiKey, apiSecret) = Configuration.Security.Keys.AlpacaApiKeyAndSecret;

            var currOptionsDictionary = m_Options.Skip((i - 1) * readAmount)
                                                 .Take(readAmount)
                                                 .ToDictionary(option => option.Ticker, option => option);

            var symbols = string.Join(",", currOptionsDictionary.Values.Select(option => option.Ticker)
                                                                .ToList());

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
                    if (pair.Value is not { DailyBar: not null, LatestQuote: not null })
                        continue;

                    var quote = pair.Value.ToQuote(currOptionsDictionary[pair.Key]);
                    quotes.Add(quote);
                }

                quotes.AddRange(body.Snapshots.Where(pair => pair.Value is { DailyBar: not null, LatestQuote: not null })
                                    .Select(pair => pair.Value.ToQuote(currOptionsDictionary[pair.Key]))
                                    .ToList());

                nextPage = body.NextPage;
            } while (!string.IsNullOrEmpty(nextPage));
        }

        await Task.WhenAll(m_RealtimeProcessors.Select(realtimeProcessor => realtimeProcessor.ProcessOptionQuotes(quotes))
                                               .ToList());
    }

    public Task OnApplicationStopped(CancellationToken _)
    {
        m_Timer.Cancel();

        return Task.CompletedTask;
    }
}

using System.Text.Json;

using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database.Processors;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Models;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

namespace Bank.ExchangeService.BackgroundServices;

public class ForexPairBackgroundService(
    IEnumerable<IRealtimeProcessor> realtimeProcessors,
    IHttpClientFactory              httpClientFactory,
    IUserServiceHttpClient          userServiceHttpClient,
    ISecurityRepository             securityRepository
)
{
    private          Timer                           m_Timer                 = null!;
    private readonly IEnumerable<IRealtimeProcessor> m_RealtimeProcessors    = realtimeProcessors;
    private readonly IHttpClientFactory              m_HttpClientFactory     = httpClientFactory;
    private readonly IUserServiceHttpClient          m_UserServiceHttpClient = userServiceHttpClient;
    private readonly ISecurityRepository             m_SecurityRepository    = securityRepository;
    private          List<CurrencySimpleResponse>    m_Currencies            = [];
    private          Dictionary<string, Security>    m_ForexPairDictionary   = [];

    public async Task OnApplicationStarted(CancellationToken cancellationToken)
    {
        m_Currencies = await m_UserServiceHttpClient.GetAllSimpleCurrencies(new CurrencyFilterQuery());

        if (m_Currencies.Count == 0)
            return;

        m_ForexPairDictionary = (await m_SecurityRepository.FindAll(SecurityType.ForexPair)).ToDictionary(forexPair => forexPair.Ticker, forexPair => forexPair);

        m_Timer = new Timer(_ => FetchQuotes()
                            .Wait(cancellationToken), null, TimeSpan.FromMinutes(Configuration.Security.Global.LatestTimeFrameInMinutes),
                            TimeSpan.FromMinutes(Configuration.Security.Global.LatestTimeFrameInMinutes));
    }

    private async Task FetchQuotes()
    {
        var httpClient = m_HttpClientFactory.CreateClient();
        var apiKey     = Configuration.Security.Keys.ApiKeyForex;
        var quotes     = new List<Quote>();

        foreach (var currencyFrom in m_Currencies)
        {
            foreach (var currencyTo in m_Currencies)
            {
                if (currencyFrom.Id == currencyTo.Id)
                    continue;

                var request = new HttpRequestMessage
                              {
                                  Method = HttpMethod.Get,
                                  RequestUri = new Uri($"{Configuration.Security.ForexPair.GetDataApi}?function=CURRENCY_EXCHANGE_RATE&from_currency={
                                      currencyFrom.Code}&to_currency={currencyTo.Code}&apikey={apiKey}"),
                                  Headers =
                                  {
                                      { "accept", "application/json" },
                                  }
                              };

                var response = await httpClient.SendAsync(request);

                if (!response.IsSuccessStatusCode)
                    return;

                var parsed = JsonDocument.Parse(await response.Content.ReadAsStringAsync());

                if (!parsed.RootElement.TryGetProperty("Realtime Currency Exchange Rate", out var forexPairElement))
                    continue;

                var body = JsonSerializer.Deserialize<FetchForexPairLatestResponse>(forexPairElement.GetRawText());

                if (body is null)
                    return;

                var ticker = $"{currencyFrom.Code}{currencyTo.Code}";
                var quote  = body.ToQuote(m_ForexPairDictionary[ticker]);
                quotes.Add(quote);
            }
        }

        //notify
        await Task.WhenAll(m_RealtimeProcessors.Select(realtimeProcessor => realtimeProcessor.ProcessForexQuotes(quotes))
                                               .ToList());
    }

    public Task OnApplicationStopped(CancellationToken _)
    {
        m_Timer.Cancel();
        return Task.CompletedTask;
    }
}

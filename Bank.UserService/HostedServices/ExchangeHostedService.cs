using Bank.Application.Endpoints;
using Bank.Application.Responses;
using Bank.UserService.Database;
using Bank.UserService.Mappers;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.HostedServices;

public class ExchangeHostedService(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
{
    private readonly IServiceProvider   m_ServiceProvider   = serviceProvider;
    private readonly IHttpClientFactory m_HttpClientFactory = httpClientFactory;
    private          Timer?             m_Timer;

    public void OnApplicationStarted()
    {
        Initialize()
        .Wait();
    }

    public void OnApplicationStopped() { }

    private async Task Initialize()
    {
        var midnight          = DateTime.Today.AddDays(1);
        var timeLeftUntilNext = midnight.Subtract(DateTime.UtcNow);

        await FetchExchangeRates();

        m_Timer = new Timer(async _ => await FetchExchangeRates(), this, timeLeftUntilNext, TimeSpan.FromDays(1));
    }


    private async Task FetchExchangeRates()
    {
        using var scope      = m_ServiceProvider.CreateScope();
        var       context    = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
        var       httpClient = m_HttpClientFactory.CreateClient(nameof(Initialize));
        var       response   = await httpClient.GetAsync(Endpoints.ExchangeRate.FetchRatesApiRsd);

        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException exception)
        {
            Console.WriteLine(exception.Message);
        }

        var exchangeRateFetchResponse = await response.Content.ReadFromJsonAsync<Dictionary<string, ExchangeRateFetchResponse>>();

        var domesticCurrency = await FindByCode("RSD", context);

        if (domesticCurrency is null)
            throw new Exception($"No Domestic Currency RSD");

        foreach (var item in exchangeRateFetchResponse)
        {
            var foreignCurrency = await FindByCode(item.Value.Code, context);

            if (foreignCurrency is null)
                continue;

            var exchangeRate = await FindByCurrencyFromCodeAndCurrencyToCode(domesticCurrency.Code, foreignCurrency.Code, context);

            if (exchangeRate is null)
                await AddWithoutSave(item.Value.ToExchangeRate(domesticCurrency, foreignCurrency), context);
            else
                await UpdateWithoutSave(exchangeRate, item.Value.ToExchangeRate(exchangeRate), context);
        }

        await context.SaveChangesAsync();
    }

    private async Task<Currency?> FindByCode(string currencyCode, ApplicationContext context)
    {
        return await context.Currencies.Include(c => c.Countries)
                            .FirstOrDefaultAsync(x => x.Code == currencyCode);
    }

    private async Task<ExchangeRate?> FindByCurrencyFromCodeAndCurrencyToCode(string firstCurrencyCode, string secondCurrencyCode, ApplicationContext context)
    {
        return await context.ExchangeRates.Include(exchangeRate => exchangeRate.CurrencyFrom)
                            .Include(exchangeRate => exchangeRate.CurrencyTo)
                            .FirstOrDefaultAsync(exchangeRate => (exchangeRate.CurrencyFrom.Code == firstCurrencyCode  && exchangeRate.CurrencyTo.Code == secondCurrencyCode) ||
                                                                 (exchangeRate.CurrencyFrom.Code == secondCurrencyCode && exchangeRate.CurrencyTo.Code == firstCurrencyCode));
    }

    public async Task<ExchangeRate> AddWithoutSave(ExchangeRate exchangeRate, ApplicationContext context)
    {
        var addExchangeRate = await context.ExchangeRates.AddAsync(exchangeRate);

        return addExchangeRate.Entity;
    }

    public async Task<ExchangeRate> UpdateWithoutSave(ExchangeRate oldExchangeRate, ExchangeRate exchangeRate, ApplicationContext context)
    {
        context.ExchangeRates.Entry(oldExchangeRate)
               .State = EntityState.Detached;

        var updatedExchangeRate = context.ExchangeRates.Update(exchangeRate);

        return updatedExchangeRate.Entity;
    }
}

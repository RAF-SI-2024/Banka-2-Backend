using Bank.Application.Extensions;
using Bank.UserService.Repositories;
using Bank.UserService.Services;

namespace Bank.UserService.BackgroundServices;

public class CurrencyExchangeBackgroundService(IDataService dataService, IExchangeRepository exchangeRepository)
{
    private          Timer?              m_Timer;
    private readonly IDataService        m_DataService        = dataService;
    private readonly IExchangeRepository m_ExchangeRepository = exchangeRepository;

    public Task OnApplicationStarted(CancellationToken cancellationToken)
    {
        var midnight          = DateTime.Today.AddDays(1);
        var timeLeftUntilNext = midnight.Subtract(DateTime.UtcNow);

        m_Timer = new Timer(_ => FetchExchanges()
                            .Wait(cancellationToken), null, timeLeftUntilNext, TimeSpan.FromDays(1));

        return Task.CompletedTask;
    }

    private async Task FetchExchanges()
    {
        var currencyExchanges = await m_DataService.GetCurrencyExchanges();

        await m_ExchangeRepository.AddRange(currencyExchanges.values);
    }

    public Task OnApplicationStopped(CancellationToken _)
    {
        m_Timer?.Cancel();

        return Task.CompletedTask;
    }
}

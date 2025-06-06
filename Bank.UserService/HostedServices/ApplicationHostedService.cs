using Bank.UserService.BackgroundServices;

namespace Bank.UserService.HostedServices;

public class ApplicationHostedService(
    LoanHostedService                 loanHostedService,
    TransactionBackgroundService      transactionBackgroundService,
    DatabaseBackgroundService         databaseBackgroundService,
    CurrencyExchangeBackgroundService currencyExchangeBackgroundService
) : IHostedService
{
    private readonly LoanHostedService                 m_LoanHostedService                 = loanHostedService;
    private readonly TransactionBackgroundService      m_TransactionBackgroundService      = transactionBackgroundService;
    private readonly DatabaseBackgroundService         m_DatabaseBackgroundService         = databaseBackgroundService;
    private readonly CurrencyExchangeBackgroundService m_CurrencyExchangeBackgroundService = currencyExchangeBackgroundService;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await m_DatabaseBackgroundService.OnApplicationStarted(cancellationToken);
        await m_CurrencyExchangeBackgroundService.OnApplicationStarted(cancellationToken);
        await m_TransactionBackgroundService.OnApplicationStarted();

        m_LoanHostedService.OnApplicationStarted();
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await m_DatabaseBackgroundService.OnApplicationStopped(cancellationToken);
        await m_CurrencyExchangeBackgroundService.OnApplicationStopped(cancellationToken);
        await m_TransactionBackgroundService.OnApplicationStopped();

        m_LoanHostedService.OnApplicationStopped();
    }
}

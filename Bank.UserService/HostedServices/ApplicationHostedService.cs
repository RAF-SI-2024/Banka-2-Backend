using Bank.Application;
using Bank.Link.Configurations;
using Bank.UserService.BackgroundServices;

using Configuration = Bank.UserService.Configurations.Configuration;

namespace Bank.UserService.HostedServices;

public class ApplicationHostedService(
    LoanHostedService                 loanHostedService,
    TransactionBackgroundService      transactionBackgroundService,
    DatabaseBackgroundService         databaseBackgroundService,
    CurrencyExchangeBackgroundService currencyExchangeBackgroundService,
    ILogger<ApplicationHostedService> logger
) : IHostedService
{
    private readonly LoanHostedService                 m_LoanHostedService                 = loanHostedService;
    private readonly TransactionBackgroundService      m_TransactionBackgroundService      = transactionBackgroundService;
    private readonly DatabaseBackgroundService         m_DatabaseBackgroundService         = databaseBackgroundService;
    private readonly CurrencyExchangeBackgroundService m_CurrencyExchangeBackgroundService = currencyExchangeBackgroundService;
    private readonly ILogger<ApplicationHostedService> m_Logger                            = logger;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await m_DatabaseBackgroundService.OnApplicationStarted(cancellationToken);
        await m_CurrencyExchangeBackgroundService.OnApplicationStarted(cancellationToken);
        await m_TransactionBackgroundService.OnApplicationStarted();

        m_LoanHostedService.OnApplicationStarted();

        m_Logger.LogInformation("{} | {} | {}", Configuration.Application.Profile, ApplicationInfo.Build.BuildDate, ApplicationInfo.Build.SourceRevisionId);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await m_DatabaseBackgroundService.OnApplicationStopped(cancellationToken);
        await m_CurrencyExchangeBackgroundService.OnApplicationStopped(cancellationToken);
        await m_TransactionBackgroundService.OnApplicationStopped();

        m_LoanHostedService.OnApplicationStopped();
    }
}

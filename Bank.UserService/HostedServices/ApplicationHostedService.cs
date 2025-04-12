using Bank.UserService.BackgroundServices;

namespace Bank.UserService.HostedServices;

public class ApplicationHostedService(
    IHostApplicationLifetime     applicationLifetime,
    DatabaseHostedService        databaseHostedService,
    ExchangeHostedService        exchangeHostedService,
    LoanHostedService            loanHostedService,
    TransactionBackgroundService transactionBackgroundService
) : IHostedService
{
    private readonly IHostApplicationLifetime     m_ApplicationLifetime          = applicationLifetime;
    private readonly DatabaseHostedService        m_DatabaseHostedService        = databaseHostedService;
    private readonly ExchangeHostedService        m_ExchangeHostedService        = exchangeHostedService;
    private readonly LoanHostedService            m_LoanHostedService            = loanHostedService;
    private readonly TransactionBackgroundService m_TransactionBackgroundService = transactionBackgroundService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        m_ApplicationLifetime.ApplicationStarted.Register(() =>
                                                          {
                                                              m_DatabaseHostedService.OnApplicationStarted();
                                                              m_ExchangeHostedService.OnApplicationStarted();
                                                              m_LoanHostedService.OnApplicationStarted();
                                                              m_TransactionBackgroundService.OnApplicationStarted();
                                                          });

        m_ApplicationLifetime.ApplicationStopped.Register(() =>
                                                          {
                                                              m_ExchangeHostedService.OnApplicationStopped();
                                                              m_DatabaseHostedService.OnApplicationStopped();
                                                              m_LoanHostedService.OnApplicationStopped();
                                                              m_TransactionBackgroundService.OnApplicationStopped();
                                                          });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

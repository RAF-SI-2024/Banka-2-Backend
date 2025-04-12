using Bank.ExchangeService.BackgroundServices;

namespace Bank.ExchangeService.HostedServices;

public class ApplicationHostedService : IHostedService
{
    private readonly IHostApplicationLifetime  m_ApplicationLifetime;
    private readonly DatabaseBackgroundService m_DatabaseBackgroundService;
    private readonly DatabaseHostedService     m_DatabaseHostedService;

    public ApplicationHostedService(IHostApplicationLifetime applicationLifetime, DatabaseBackgroundService databaseBackgroundService, DatabaseHostedService databaseHostedService)
    {
        m_ApplicationLifetime       = applicationLifetime;
        m_DatabaseBackgroundService = databaseBackgroundService;
        m_DatabaseHostedService     = databaseHostedService;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        m_ApplicationLifetime.ApplicationStarted.Register(async () =>
                                                          {
                                                              m_DatabaseBackgroundService.OnApplicationStarted();
                                                              await m_DatabaseHostedService.OnApplicationStarted();
                                                          });

        m_ApplicationLifetime.ApplicationStopped.Register(() =>
                                                          {
                                                              m_DatabaseBackgroundService.OnApplicationStopped();
                                                              m_DatabaseHostedService.OnApplicationStopped();
                                                          });

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

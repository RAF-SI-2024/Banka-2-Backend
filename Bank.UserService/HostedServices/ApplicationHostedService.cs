namespace Bank.UserService.HostedServices;

public class ApplicationHostedService(IHostApplicationLifetime applicationLifetime, DatabaseHostedService databaseHostedService) : IHostedService
{
    private readonly IHostApplicationLifetime m_ApplicationLifetime   = applicationLifetime;
    private readonly DatabaseHostedService    m_DatabaseHostedService = databaseHostedService;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        m_ApplicationLifetime.ApplicationStarted.Register(m_DatabaseHostedService.OnApplicationStarted);
        m_ApplicationLifetime.ApplicationStopped.Register(m_DatabaseHostedService.OnApplicationStopped);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}

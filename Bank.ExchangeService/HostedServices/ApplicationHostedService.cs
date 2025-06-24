using Bank.Application;
using Bank.ExchangeService.BackgroundServices;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database.Processors;

namespace Bank.ExchangeService.HostedServices;

public class ApplicationHostedService(
    DatabaseBackgroundService         databaseBackgroundService,
    IEnumerable<IRealtimeProcessor>   realtimeProcessors,
    StockBackgroundService            stockBackgroundService,
    ForexPairBackgroundService        forexPairBackgroundService,
    OptionBackgroundService           optionBackgroundService,
    ILogger<ApplicationHostedService> logger
) : IHostedService
{
    private readonly DatabaseBackgroundService         m_DatabaseBackgroundService  = databaseBackgroundService;
    private readonly IEnumerable<IRealtimeProcessor>   m_RealtimeProcessors         = realtimeProcessors;
    private readonly StockBackgroundService            m_StockBackgroundService     = stockBackgroundService;
    private readonly ForexPairBackgroundService        m_ForexPairBackgroundService = forexPairBackgroundService;
    private readonly OptionBackgroundService           m_OptionBackgroundService    = optionBackgroundService;
    private readonly ILogger<ApplicationHostedService> m_Logger                     = logger;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        m_DatabaseBackgroundService.OnApplicationStarted();
        await m_StockBackgroundService.OnApplicationStarted(cancellationToken);
        await m_ForexPairBackgroundService.OnApplicationStarted(cancellationToken);
        await m_OptionBackgroundService.OnApplicationStarted(cancellationToken);

        await Task.WhenAll(m_RealtimeProcessors.Select(realtimeProcessor => realtimeProcessor.OnApplicationStarted(cancellationToken)));
        
        m_Logger.LogInformation("{} | {} | {}", Configuration.Application.Profile, ApplicationInfo.Build.BuildDate, ApplicationInfo.Build.SourceRevisionId);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        m_DatabaseBackgroundService.OnApplicationStopped();
        await m_StockBackgroundService.OnApplicationStopped(cancellationToken);
        await m_ForexPairBackgroundService.OnApplicationStopped(cancellationToken);
        await m_OptionBackgroundService.OnApplicationStopped(cancellationToken);

        await Task.WhenAll(m_RealtimeProcessors.Select(realtimeProcessor => realtimeProcessor.OnApplicationStopped(cancellationToken)));
    }
}

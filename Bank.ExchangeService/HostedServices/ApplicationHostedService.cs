using Bank.ExchangeService.BackgroundServices;
using Bank.ExchangeService.Database.Processors;

namespace Bank.ExchangeService.HostedServices;

public class ApplicationHostedService(
    DatabaseBackgroundService       databaseBackgroundService,
    IEnumerable<IRealtimeProcessor> realtimeProcessors,
    StockBackgroundService          stockBackgroundService,
    ForexPairBackgroundService      forexPairBackgroundService,
    OptionBackgroundService         optionBackgroundService
) : IHostedService
{
    private readonly DatabaseBackgroundService       m_DatabaseBackgroundService  = databaseBackgroundService;
    private readonly IEnumerable<IRealtimeProcessor> m_RealtimeProcessors         = realtimeProcessors;
    private readonly StockBackgroundService          m_StockBackgroundService     = stockBackgroundService;
    private readonly ForexPairBackgroundService      m_ForexPairBackgroundService = forexPairBackgroundService;
    private readonly OptionBackgroundService         m_OptionBackgroundService    = optionBackgroundService;

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        m_DatabaseBackgroundService.OnApplicationStarted();
        await m_StockBackgroundService.OnApplicationStarted(cancellationToken);
        await m_ForexPairBackgroundService.OnApplicationStarted(cancellationToken);
        await m_OptionBackgroundService.OnApplicationStarted(cancellationToken);

        await Task.WhenAll(m_RealtimeProcessors.Select(realtimeProcessor => realtimeProcessor.OnApplicationStarted(cancellationToken)));
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

using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database;

namespace Bank.ExchangeService.BackgroundServices;

public class DatabaseBackgroundService(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider m_ServiceProvider = serviceProvider;

    private DatabaseContext Context =>
    m_ServiceProvider.CreateScope()
                     .ServiceProvider.GetRequiredService<DatabaseContext>();

    public void OnApplicationStarted()
    {
        if (Configuration.Database.CreateDrop)
            Context.Database.EnsureDeletedAsync()
                   .Wait();

        Context.Database.EnsureCreatedAsync()
               .Wait();
    }

    public void OnApplicationStopped() { }
}

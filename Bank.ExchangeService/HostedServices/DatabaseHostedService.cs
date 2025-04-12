using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.Database.Seeders;
using Bank.ExchangeService.Database.Seeders.Resource;

namespace Bank.ExchangeService.HostedServices;

public class DatabaseHostedService
{
    private readonly IHttpClientFactory m_HttpClientFactory;
    private readonly IServiceProvider   m_ServiceProvider;

    public DatabaseHostedService(IServiceProvider serviceProvider, IHttpClientFactory httpClientFactory)
    {
        m_HttpClientFactory = httpClientFactory;
        m_ServiceProvider   = serviceProvider;
    }

    private DatabaseContext Context =>
    m_ServiceProvider.CreateScope()
                     .ServiceProvider.GetRequiredService<DatabaseContext>();

    public async Task OnApplicationStarted()
    {
        if (Configuration.Database.CreateDrop)
            await Context.Database.EnsureDeletedAsync();

        await Context.Database.EnsureCreatedAsync();

        await Context.SeedStockExchanges();
        await Context.SeedListings(); 
        await Context.SeedListingHistoricals();
    }

    public void OnApplicationStopped() { }
}

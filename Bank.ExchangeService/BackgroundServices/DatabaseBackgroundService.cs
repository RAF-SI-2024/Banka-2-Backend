using Bank.Application.Domain;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.Database.Seeders;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

namespace Bank.ExchangeService.BackgroundServices;

public class DatabaseBackgroundService(
    IServiceProvider       serviceProvider,
    IHttpClientFactory     httpClientFactory,
    IUserServiceHttpClient userServiceHttpClient,
    IRedisRepository       redisRepository
)
{
    private readonly IServiceProvider       m_ServiceProvider       = serviceProvider;
    private readonly IHttpClientFactory     m_HttpClientFactory     = httpClientFactory;
    private readonly IUserServiceHttpClient m_UserServiceHttpClient = userServiceHttpClient;
    private readonly IRedisRepository       m_RedisRepository       = redisRepository;
    private          ISecurityRepository    m_SecurityRepository    = null!;
    private          IQuoteRepository       m_QuoteRepository       = null!;
    private          bool                   m_IsProcessRunning      = false;
    private          int                    m_IterationCount        = 0;

    private DatabaseContext Context =>
    m_ServiceProvider.CreateScope()
                     .ServiceProvider.GetRequiredService<DatabaseContext>();

    public void OnApplicationStarted()
    {
        m_SecurityRepository = m_ServiceProvider.CreateScope()
                                                .ServiceProvider.GetRequiredService<ISecurityRepository>();

        m_QuoteRepository = m_ServiceProvider.CreateScope()
                                             .ServiceProvider.GetRequiredService<IQuoteRepository>();

        if (Configuration.Database.CreateDrop)
            Context.Database.EnsureDeletedAsync()
                   .Wait();

        Context.Database.EnsureCreatedAsync()
               .Wait();

        using var client = m_HttpClientFactory.CreateClient();

        if (Configuration.Application.Profile == Profile.Testing)
        {
            Context.SeedHardcodedStockExchanges()
                   .Wait();

            Context.SeedFutureContractHardcoded()
                   .Wait();

            Context.SeedForexPairHardcoded()
                   .Wait();

            Context.SeedStockHardcoded()
                   .Wait();

            Context.SeedOptionHardcoded()
                   .Wait();

            Context.SeedOrdersHardcoded()
                   .Wait();

            Context.SeedAssetsHardcoded()
                   .Wait();

            return;
        }

        Console.WriteLine("Wait for 'Seeding Completed' message");

        m_RedisRepository.Clear();

        Context.SeedStockExchanges()
               .Wait();

        Context.SeedFutureContractsAndQuotes(m_SecurityRepository, m_QuoteRepository)
               .Wait();
        
        Context.SeedStock(client)
               .Wait();
        
        Context.SeedForexPair(client, m_UserServiceHttpClient, m_SecurityRepository)
               .Wait();
        
        Context.SeedOptionsAndQuotes(client, m_SecurityRepository, m_QuoteRepository)
               .Wait();
        
        Context.SeedForexPairQuotes(client, m_UserServiceHttpClient, m_SecurityRepository, m_QuoteRepository)
               .Wait();
        
        Context.SeedStockQuotes(client, m_SecurityRepository, m_QuoteRepository)
               .Wait();

        Console.WriteLine("Seeding Completed");
    }

    public void OnApplicationStopped() { }
}

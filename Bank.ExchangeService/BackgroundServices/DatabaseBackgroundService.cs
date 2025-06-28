using Bank.Application.Domain;
using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Database;
using Bank.ExchangeService.Database.Seeders;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

namespace Bank.ExchangeService.BackgroundServices;

public class DatabaseBackgroundService(
    IServiceProvider                   serviceProvider,
    IHttpClientFactory                 httpClientFactory,
    IUserServiceHttpClient             userServiceHttpClient,
    IRedisRepository                   redisRepository,
    ILogger<DatabaseBackgroundService> logger
)
{
    private readonly ILogger<DatabaseBackgroundService> m_Logger                = logger;
    private readonly IServiceProvider                   m_ServiceProvider       = serviceProvider;
    private readonly IHttpClientFactory                 m_HttpClientFactory     = httpClientFactory;
    private readonly IUserServiceHttpClient             m_UserServiceHttpClient = userServiceHttpClient;
    private readonly IRedisRepository                   m_RedisRepository       = redisRepository;
    private          ISecurityRepository                m_SecurityRepository    = null!;
    private          IQuoteRepository                   m_QuoteRepository       = null!;

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
        
        m_RedisRepository.Clear();

        m_Logger.LogInformation("Persistent Seeding | Start");
        m_Logger.LogInformation("Persistent Seeding | Security | Stock Exchange | Start");
        
        Context.SeedStockExchanges()
               .Wait();

        m_Logger.LogInformation("Persistent Seeding | Security | Stock Exchange | Complete");
        m_Logger.LogInformation("Persistent Seeding | Security & Quotes | Future Contract | Start");
        
        Context.SeedFutureContractsAndQuotes(m_SecurityRepository, m_QuoteRepository)
               .Wait();
        
        m_Logger.LogInformation("Persistent Seeding | Security & Quotes | Future Contract | Complete");
        m_Logger.LogInformation("Persistent Seeding | Security | Stock | Start");
        
        Context.SeedStock(client)
               .Wait();
        
        m_Logger.LogInformation("Persistent Seeding | Security | Stock | Complete");
        m_Logger.LogInformation("Persistent Seeding | Security | ForexPair | Start");
        
        Context.SeedForexPair(client, m_UserServiceHttpClient, m_SecurityRepository)
               .Wait();

        m_Logger.LogInformation("Persistent Seeding | Security | ForexPair | Complete");
        m_Logger.LogInformation("Persistent Seeding | Security & Quotes | Options | Start");
        
        Context.SeedOptionsAndQuotes(client, m_SecurityRepository, m_QuoteRepository)
               .Wait();

        m_Logger.LogInformation("Persistent Seeding | Security & Quotes | Options  | Complete");
        m_Logger.LogInformation("Persistent Seeding | Quotes | ForexPair | Start");
        
        Context.SeedForexPairQuotes(client, m_UserServiceHttpClient, m_SecurityRepository, m_QuoteRepository)
               .Wait();

        m_Logger.LogInformation("Persistent Seeding | Quotes | ForexPair | Complete");
        m_Logger.LogInformation("Persistent Seeding | Quotes | Stock | Start");
        
        Context.SeedStockQuotes(client, m_SecurityRepository, m_QuoteRepository)
               .Wait();
        
        m_Logger.LogInformation("Persistent Seeding | Quotes | Stock | Complete");
        m_Logger.LogInformation("Persistent Seeding | Complete");
    }

    public void OnApplicationStopped() { }
}

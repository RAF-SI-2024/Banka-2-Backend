using Bank.Database.Configurations;
using Bank.Database.Core;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

namespace Bank.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseServices<TDatabaseContext>(this IServiceCollection services) where TDatabaseContext : DatabaseContext
    {
        services.AddSingleton<IDatabasePoolInfo, PostgresDatabasePoolInfo>();
        services.AddSingleton<IDatabaseContextFactory<TDatabaseContext>, PostgresDefaultContextFactory<TDatabaseContext>>();

        services.AddDbContextFactory<TDatabaseContext>(options => options.UseNpgsql(Configuration.Database.Persistent.GetConnectionString(),
                                                                                    pgOptions => pgOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        return services;
    }

    public static IServiceCollection AddInMemoryDatabaseServices(this IServiceCollection services)
    {
        services.AddDistributedMemoryCache();

        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(Configuration.Database.InMemory.GetConnectionString()));

        services.AddStackExchangeRedisCache(options =>
                                            {
                                                options.Configuration = Configuration.Database.InMemory.GetConnectionString();
                                                options.InstanceName  = Configuration.Database.InMemory.Scheme;
                                            });

        return services;
    }
}

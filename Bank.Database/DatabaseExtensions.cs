using Bank.Database.Context;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Bank.Database;

public static class DatabaseExtensions
{
    public static IServiceCollection AddDatabaseContext<TDatabaseContext>(this IServiceCollection services, Action<DbContextOptionsBuilder>? optionsAction = null) where TDatabaseContext : DatabaseContext
    {
        services.AddSingleton<IDatabaseContextPool<TDatabaseContext>, PostgresContextPool<TDatabaseContext>>();
        services.AddSingleton<IDatabaseContextFactory<TDatabaseContext>, PostgresContextFactory<TDatabaseContext>>();
        services.AddDbContextFactory<TDatabaseContext>(optionsAction);

        return services;
    }
}

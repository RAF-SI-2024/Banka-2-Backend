using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

using Npgsql;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure.Internal;

namespace Bank.Database.Context;

public interface IDatabaseContextPool<TDatabaseContext> where TDatabaseContext : DatabaseContext
{
    Task Initialise();

    ConcurrentQueue<TDatabaseContext> OpenedConnectionQueue { get; }
    ConcurrentQueue<TDatabaseContext> ClosedConnectionQueue { get; }
    SemaphoreSlim                     Semaphore             { get; }
    Lock                              Lock                  { get; }
    int                               MinConnections        { get; }
    int                               MaxConnections        { get; }
}

internal class PostgresContextPool<TDatabaseContext>(DbContextOptions<TDatabaseContext> contextOptions)
: IDatabaseContextPool<TDatabaseContext> where TDatabaseContext : DatabaseContext
{
    public readonly DbContextOptions<TDatabaseContext> ContextOptions = contextOptions;

    public ConcurrentQueue<TDatabaseContext> OpenedConnectionQueue { get; } = new();
    public ConcurrentQueue<TDatabaseContext> ClosedConnectionQueue { get; } = new();

    public Lock          Lock      { get; }              = new();
    public SemaphoreSlim Semaphore { private set; get; } = null!;

    public int MinConnections { private set; get; }
    public int MaxConnections { private set; get; }

    private bool m_Initialised = false;

    [SuppressMessage("Usage",     "EF1001:Internal EF Core API usage.")]
    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
    public async Task Initialise()
    {
        if (m_Initialised)
            return;

        lock (Lock)
        {
            if (m_Initialised)
                return;

            m_Initialised = true;
        }

        await using var connection = new NpgsqlConnection(ContextOptions.FindExtension<NpgsqlOptionsExtension>()!.ConnectionString);
        await connection.OpenAsync();

        await using var command = new NpgsqlCommand("SHOW max_connections", connection);

        MaxConnections = int.Parse((string)(await command.ExecuteScalarAsync())!) - 10;
        MinConnections = MaxConnections / 2;

        Semaphore = new SemaphoreSlim(MaxConnections);
    }
}

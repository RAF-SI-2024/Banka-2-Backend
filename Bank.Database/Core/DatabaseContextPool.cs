using System.Diagnostics.CodeAnalysis;

using Bank.Database.Configurations;

using Npgsql;

namespace Bank.Database.Core;

public interface IDatabasePoolInfo
{
    int MinConnections { get; }
    int MaxConnections { get; }
}

internal class PostgresDatabasePoolInfo : IDatabasePoolInfo
{
    public const int ConnectionExcess = 10;
    public       int MinConnections { private set; get; } = Configuration.Database.Persistent.MinConnections;
    public       int MaxConnections { private set; get; } = Configuration.Database.Persistent.MaxConnections;

    public PostgresDatabasePoolInfo()
    {
        Initialise()
        .Wait();
    }

    [SuppressMessage("Usage",     "EF1001:Internal EF Core API usage.")]
    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
    public async Task Initialise()
    {
        if (MaxConnections == 0)
        {
            await using var connection = new NpgsqlConnection(Configuration.Database.Persistent.GetConnectionString());
            await connection.OpenAsync();

            await using var command = new NpgsqlCommand("SHOW max_connections", connection);

            MaxConnections = int.Parse((string)(await command.ExecuteScalarAsync())!) - ConnectionExcess;
        }

        if (MinConnections == 0)
            MinConnections = MaxConnections / 2;
    }
}

using Microsoft.EntityFrameworkCore;

namespace Bank.Database.Context;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    internal Action<DatabaseContext>          DisposeAction      { get; set; } = null!;
    internal Func<DatabaseContext, ValueTask> DisposeActionAsync { get; set; } = null!;

    public override void Dispose() => DisposeAction(this);

    public override async ValueTask DisposeAsync() => await DisposeActionAsync(this);
}

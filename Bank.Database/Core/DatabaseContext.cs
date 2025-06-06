using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

namespace Bank.Database.Core;

public class DatabaseContext(DbContextOptions options) : DbContext(options)
{
    internal bool       BaseDispose              { get; set; } = true;
    internal Action     DisposeBeforeAction      { get; set; } = () => { };
    internal Action     DisposeAfterAction       { get; set; } = () => { };
    internal Func<Task> DisposeBeforeActionAsync { get; set; } = () => Task.CompletedTask;
    internal Func<Task> DisposeAfterActionAsync  { get; set; } = () => Task.CompletedTask;

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize")]
    public override void Dispose()
    {
        DisposeBeforeAction();

        if (BaseDispose)
            base.Dispose();

        DisposeAfterAction();
    }

    [SuppressMessage("Usage", "CA1816:Dispose methods should call SuppressFinalize")]
    public override async ValueTask DisposeAsync()
    {
        await DisposeBeforeActionAsync();

        if (BaseDispose)
            await base.DisposeAsync();

        await DisposeAfterActionAsync();
    }
}

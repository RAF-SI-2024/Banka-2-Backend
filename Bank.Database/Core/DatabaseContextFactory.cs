using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

using Microsoft.EntityFrameworkCore;

namespace Bank.Database.Core;

public interface IDatabaseContextFactory<TDatabaseContext> where TDatabaseContext : DatabaseContext
{
    Task<TDatabaseContext> CreateContext            { get; }
    Task<TDatabaseContext> CreateDistributedContext { get; }
}

#region Ado.NET Pooling

internal class PostgresDefaultContextFactory<TDatabaseContext>(IDatabasePoolInfo poolInfo, IDbContextFactory<TDatabaseContext> contextFactory)
: IDatabaseContextFactory<TDatabaseContext> where TDatabaseContext : DatabaseContext
{
    private readonly IDbContextFactory<TDatabaseContext> m_ContextFactory = contextFactory;

    private readonly IDatabasePoolInfo m_PoolInfo             = poolInfo;
    private readonly SemaphoreSlim     m_SemaphoreDefault     = new(poolInfo.MaxConnections);
    private readonly SemaphoreSlim     m_SemaphoreDistributed = new(poolInfo.MaxConnections / 10);

    public Task<TDatabaseContext> CreateContext            => GetOrCreateContext();
    public Task<TDatabaseContext> CreateDistributedContext => GetOrCreateDistributedContext();

    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
    private async Task<TDatabaseContext> GetOrCreateContext()
    {
        await m_SemaphoreDefault.WaitAsync();

        var context = await m_ContextFactory.CreateDbContextAsync();

        context.DisposeAfterAction      = () => DisposeAfterAction(context);
        context.DisposeAfterActionAsync = () => DisposeAfterActionAsync(context);

        return context;
    }

    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
    private async Task<TDatabaseContext> GetOrCreateDistributedContext()
    {
        await m_SemaphoreDistributed.WaitAsync();
        await m_SemaphoreDefault.WaitAsync();

        var context = await m_ContextFactory.CreateDbContextAsync();

        context.DisposeAfterAction      = () => DisposeDistributedAfterAction(context);
        context.DisposeAfterActionAsync = () => DisposeDistributedAfterActionAsync(context);

        return context;
    }

    private async Task DisposeDistributedAfterActionAsync(TDatabaseContext context)
    {
        m_SemaphoreDefault.Release();
        m_SemaphoreDistributed.Release();
    }

    private void DisposeDistributedAfterAction(TDatabaseContext context)
    {
        m_SemaphoreDefault.Release();
        m_SemaphoreDistributed.Release();
    }

    private async Task DisposeAfterActionAsync(TDatabaseContext context)
    {
        m_SemaphoreDefault.Release();
    }

    private void DisposeAfterAction(TDatabaseContext context)
    {
        m_SemaphoreDefault.Release();
    }
}

#endregion

#region Custom Pooling

internal class PostgresDatabaseContextFactory<TDatabaseContext>(IDatabasePoolInfo poolInfo, IDbContextFactory<TDatabaseContext> contextFactory)
: IDatabaseContextFactory<TDatabaseContext> where TDatabaseContext : DatabaseContext
{
    private readonly IDbContextFactory<TDatabaseContext> m_ContextFactory = contextFactory;

    private readonly ConcurrentQueue<TDatabaseContext> m_OpenedConnectionQueue = new();
    private readonly ConcurrentQueue<TDatabaseContext> m_ClosedConnectionQueue = new();
    private readonly Lock                              m_Lock                  = new();

    private readonly IDatabasePoolInfo m_PoolInfo             = poolInfo;
    private readonly SemaphoreSlim     m_SemaphoreDefault     = new(poolInfo.MaxConnections);
    private readonly SemaphoreSlim     m_SemaphoreDistributed = new(poolInfo.MaxConnections / 10);

    public Task<TDatabaseContext> CreateContext            => GetOrCreateContext();
    public Task<TDatabaseContext> CreateDistributedContext => GetOrCreateDistributedContext();

    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
    private async Task<TDatabaseContext> GetOrCreateContext()
    {
        await m_SemaphoreDefault.WaitAsync();

        if (m_OpenedConnectionQueue.TryDequeue(out var openedContext))
        {
            return openedContext;
        }

        if (m_ClosedConnectionQueue.TryDequeue(out var closedContext))
        {
            await closedContext.Database.OpenConnectionAsync();

            return closedContext;
        }

        var context = await m_ContextFactory.CreateDbContextAsync();

        context.BaseDispose             = false;
        context.DisposeAfterAction      = () => DisposeAfterAction(context);
        context.DisposeAfterActionAsync = () => DisposeAfterActionAsync(context);

        return context;
    }

    [SuppressMessage("ReSharper", "InconsistentlySynchronizedField")]
    private async Task<TDatabaseContext> GetOrCreateDistributedContext()
    {
        await m_SemaphoreDefault.WaitAsync();

        if (m_OpenedConnectionQueue.TryDequeue(out var openedContext))
            return openedContext;

        if (m_ClosedConnectionQueue.TryDequeue(out var closedContext))
        {
            await closedContext.Database.OpenConnectionAsync();

            return closedContext;
        }

        var context = await m_ContextFactory.CreateDbContextAsync();

        context.BaseDispose             = false;
        context.DisposeAfterAction      = () => DisposeDistributedAfterAction(context);
        context.DisposeAfterActionAsync = () => DisposeDistributedAfterActionAsync(context);

        return context;
    }

    private async Task DisposeDistributedAfterActionAsync(TDatabaseContext context)
    {
        await CacheContextAsync(context);

        m_SemaphoreDefault.Release();
        m_SemaphoreDistributed.Release();
    }

    private void DisposeDistributedAfterAction(TDatabaseContext context)
    {
        CacheContext(context);

        m_SemaphoreDefault.Release();
        m_SemaphoreDistributed.Release();
    }

    private async Task DisposeAfterActionAsync(TDatabaseContext context)
    {
        await CacheContextAsync(context);

        m_SemaphoreDefault.Release();
    }

    private void DisposeAfterAction(TDatabaseContext context)
    {
        CacheContext(context);

        m_SemaphoreDefault.Release();
    }

    private async Task CacheContextAsync(TDatabaseContext context)
    {
        Task? closeConnectionTask = null;

        lock (m_Lock)
            if (m_OpenedConnectionQueue.Count < m_PoolInfo.MinConnections)
                m_OpenedConnectionQueue.Enqueue(context);
            else
            {
                closeConnectionTask = context.Database.CloseConnectionAsync();

                m_ClosedConnectionQueue.Enqueue(context);
            }

        if (closeConnectionTask != null)
            await closeConnectionTask;
    }

    private void CacheContext(TDatabaseContext context)
    {
        lock (m_Lock)
            if (m_OpenedConnectionQueue.Count < m_PoolInfo.MinConnections)
                m_OpenedConnectionQueue.Enqueue(context);
            else
            {
                context.Database.CloseConnection();

                m_ClosedConnectionQueue.Enqueue(context);
            }
    }
}

#endregion

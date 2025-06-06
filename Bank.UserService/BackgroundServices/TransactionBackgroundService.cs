using System.Collections.Concurrent;

using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.UserService.Configurations;
using Bank.UserService.Models;
using Bank.UserService.Services;

namespace Bank.UserService.BackgroundServices;

public class TransactionBackgroundService(ITransactionService transactionService)
{
    private readonly ITransactionService m_TransactionService = transactionService;

    public ConcurrentQueue<ProcessTransaction> InternalTransactions { get; } = new();
    public ConcurrentQueue<ProcessTransaction> ExternalTransactions { get; } = new();

    private Timer m_InternalTimer = null!;
    private Timer m_ExternalTimer = null!;

    public async Task OnApplicationStarted()
    {
        if (Configuration.Application.Profile == Profile.Testing)
            return;

        return;

        m_InternalTimer = new Timer(service => ProcessInternalTransactions(service)
                                    .Wait(), this, TimeSpan.Zero, TimeSpan.FromSeconds(15));

        m_ExternalTimer = new Timer(service => ProcessExternalTransactions(service)
                                    .Wait(), this, TimeSpan.Zero, TimeSpan.FromMinutes(15));
    }

    private bool m_ProcessingInternalTransaction = false;
    private bool m_ProcessingExternalTransaction = false;

    public async Task ProcessInternalTransactions(object? _)
    {
        if (m_ProcessingInternalTransaction || InternalTransactions.IsEmpty)
            return;

        m_ProcessingInternalTransaction = true;

        var processTransactions = new List<ProcessTransaction>();

        while (InternalTransactions.TryDequeue(out var processTransaction))
            processTransactions.Add(processTransaction);

        await Task.WhenAll(processTransactions.Select(m_TransactionService.ProcessInternalTransaction)
                                              .ToList());

        m_ProcessingInternalTransaction = false;
    }

    public async Task ProcessExternalTransactions(object? _)
    {
        if (m_ProcessingExternalTransaction || ExternalTransactions.IsEmpty)
            return;

        m_ProcessingExternalTransaction = true;

        var processTransactions = new List<ProcessTransaction>();

        while (ExternalTransactions.TryDequeue(out var processTransaction))
            processTransactions.Add(processTransaction);

        await Task.WhenAll(processTransactions.Select(m_TransactionService.ProcessExternalTransaction)
                                              .ToList());

        m_ProcessingExternalTransaction = false;
    }

    public Task OnApplicationStopped()
    {
        return Task.CompletedTask;

        m_InternalTimer.Cancel();
        m_ExternalTimer.Cancel();

        return Task.CompletedTask;
    }
}

﻿using System.Collections.Concurrent;

using Bank.Application.Extensions;
using Bank.UserService.Configurations;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;
using Bank.UserService.Repositories;
using Bank.UserService.Services;

namespace Bank.UserService.BackgroundServices;

using BankModel = Models.Bank;

public class TransactionBackgroundService(IServiceProvider serviceProvider)
{
    private readonly IServiceProvider m_ServiceProvider = serviceProvider;

    public BankModel Bank            { private set; get; } = null!;
    public Currency  DefaultCurrency { private set; get; } = null!;
    public Account   BankAccount     { private set; get; } = null!;

    private IServiceScope       CreateScope        => m_ServiceProvider.CreateScope();
    private ITransactionService TransactionService => CreateScope.ServiceProvider.GetRequiredService<ITransactionService>();

    public ConcurrentQueue<ProcessTransaction> InternalTransactions { get; } = new();
    public ConcurrentQueue<ProcessTransaction> ExternalTransactions { get; } = new();

    private Timer m_InternalTimer = null!;
    private Timer m_ExternalTimer = null!;

    public void OnApplicationStarted()
    {
        var bankRepository     = CreateScope.ServiceProvider.GetRequiredService<IBankRepository>();
        var currencyRepository = CreateScope.ServiceProvider.GetRequiredService<ICurrencyRepository>();
        var accountRepository  = CreateScope.ServiceProvider.GetRequiredService<IAccountRepository>();

        Bank = bankRepository.FindById(Seeder.Bank.Bank02.Id)
                             .Result ?? throw new Exception("Invalid bank.");

        DefaultCurrency = currencyRepository.FindByCode(Configuration.Exchange.DefaultCurrencyCode)
                                            .Result ?? throw new Exception("Invalid default currency.");

        BankAccount = accountRepository.FindById(Seeder.Account.BankAccount.Id)
                                       .Result ?? throw new Exception("Invalid bank account.");

        m_InternalTimer = new Timer(service => ProcessInternalTransactions(service).Wait(), this, TimeSpan.Zero, TimeSpan.FromSeconds(15));
        m_ExternalTimer = new Timer(service => ProcessExternalTransactions(service).Wait(), this, TimeSpan.Zero, TimeSpan.FromMinutes(15));
    }

    private bool m_ProcessingInternalTransaction = false;

    private async Task ProcessInternalTransactions(object? @object)
    {
        if (m_ProcessingInternalTransaction || InternalTransactions.IsEmpty)
            return;

        m_ProcessingInternalTransaction = true;

        var transactionBackgroundService = @object as TransactionBackgroundService;

        var transactionsPerAccount = new ConcurrentDictionary<Guid, List<ProcessTransaction>>();

        while (InternalTransactions.TryDequeue(out var transaction))
        {
            transactionsPerAccount.GetOrAdd(transaction.FromAccountId, [])
                                  .Add(transaction);
            
            transactionsPerAccount.GetOrAdd(transaction.ToAccountId, [])
                                  .Add(transaction);
        }

        var transactionService = transactionBackgroundService!.TransactionService;

        foreach (var accountTransactions in transactionsPerAccount)
            await transactionService.ProcessInternalTransactionsForAccount(accountTransactions.Key, accountTransactions.Value);

        m_ProcessingInternalTransaction = false;
    }

    private async Task ProcessExternalTransactions(object? @object)
    {
        var transactionBackgroundService = @object as TransactionBackgroundService;
    }

    public void OnApplicationStopped()
    {
        m_InternalTimer.Cancel();
        m_ExternalTimer.Cancel();
    }
}

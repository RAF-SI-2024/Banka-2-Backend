using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.UserService.Database;
using Bank.UserService.Models;
using Bank.UserService.Services;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;

namespace Bank.UserService.Repositories;

public interface IAccountRepository
{
    Task<Page<Account>> FindAll(AccountFilterQuery accountFilterQuery, Pageable pageable);

    Task<Page<Account>> FindAllByClientId(Guid clientId, Pageable pageable);

    Task<Account?> FindById(Guid id);

    Task<Account?> FindByNumber(string number);

    Task<Account?> FindByAccountNumber(string? accountNumber);

    Task<Account> Add(Account account);

    Task<bool> AddRange(IEnumerable<Account> accounts);

    Task<Account> Update(Account account);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();

    Task<bool> IncreaseBalance(Guid accountId, decimal amount, ApplicationContext? context = null);

    Task<bool> DecreaseBalance(Guid accountId, decimal amount, ApplicationContext? context = null);

    Task<bool> DecreaseBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext? context = null);

    Task<bool> DecreaseAvailableBalance(Guid accountId, decimal amount, ApplicationContext? context = null);

    Task<bool> DecreaseAvailableBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext? context = null);

    Task<bool> DecreaseAvailableBalanceWithoutLimitCheck(Guid accountId, decimal amount, ApplicationContext? context = null);

    Task<bool> DecreaseAvailableBalanceWithoutLimitCheck(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext? context = null);

    Task<bool> IncreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount, ApplicationContext? context = null);

    Task<bool> DecreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount, ApplicationContext? context = null);
}

public class AccountRepository(IDatabaseContextFactory<ApplicationContext> contextFactory, Lazy<IDataService> dataServiceLazy) : IAccountRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory  = contextFactory;
    private readonly Lazy<IDataService>                          m_DataServiceLazy = dataServiceLazy;

    private IDataService Data => m_DataServiceLazy.Value;

    private static DateTime Midnight => DateTime.UtcNow.Date;

    private static DateTime FirstDayOfMonth =>
    DateTime.UtcNow.AddDays(1 - DateTime.UtcNow.Day)
            .Date;

    public async Task<Page<Account>> FindAll(AccountFilterQuery accountFilterQuery, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var accountQuery = context.Accounts.IncludeAll()
                                  .AsQueryable();

        if (!string.IsNullOrEmpty(accountFilterQuery.ClientEmail))
            accountQuery = accountQuery.Where(account => account.Client != null && EF.Functions.ILike(account.Client.Email, $"%{accountFilterQuery.ClientEmail}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.ClientFirstName))
            accountQuery = accountQuery.Where(account => account.Client != null && EF.Functions.ILike(account.Client.FirstName, $"%{accountFilterQuery.ClientFirstName}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.ClientLastName))
            accountQuery = accountQuery.Where(account => account.Client != null && EF.Functions.ILike(account.Client.LastName, $"%{accountFilterQuery.ClientLastName}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.AccountTypeName))
            accountQuery = accountQuery.Where(account => account.Type != null && EF.Functions.ILike(account.Type.Name, $"%{accountFilterQuery.AccountTypeName}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.CurrencyName))
            accountQuery = accountQuery.Where(account => account.Currency != null && EF.Functions.ILike(account.Currency.Name, $"%{accountFilterQuery.CurrencyName}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.EmployeeEmail))
            accountQuery = accountQuery.Where(account => account.Employee != null && EF.Functions.ILike(account.Employee.Email, $"%{accountFilterQuery.EmployeeEmail}%"));

        if (!string.IsNullOrEmpty(accountFilterQuery.Number))
            accountQuery = accountQuery.Where(account => EF.Functions.ILike(account.Number, $"%{accountFilterQuery.Number}%"));

        if (accountFilterQuery.Status != null)
            accountQuery = accountQuery.Where(account => account.Status == accountFilterQuery.Status.Value);

        var accounts = await accountQuery.Skip((pageable.Page - 1) * pageable.Size)
                                         .Take(pageable.Size)
                                         .ToListAsync();

        var totalElements = await accountQuery.CountAsync();

        return new Page<Account>(accounts, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Page<Account>> FindAllByClientId(Guid clientId, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var accounts = await context.Accounts.IncludeAll()
                                    .Where(account => account.ClientId == clientId)
                                    .Skip((pageable.Page - 1) * pageable.Size)
                                    .Take(pageable.Size)
                                    .ToListAsync();

        var totalElements = await context.Accounts.CountAsync();

        return new Page<Account>(accounts, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Account?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Accounts.IncludeAll()
                            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Accounts.AnyAsync(account => account.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Accounts.AnyAsync() is not true;
    }

    public async Task<Account?> FindByNumber(string number)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Accounts.IncludeAll()
                            .FirstOrDefaultAsync(account => account.Number == number);
    }

    public async Task<Account?> FindByAccountNumber(string? accountNumber)
    {
        if (accountNumber is null)
            return null;

        await using var context = await m_ContextFactory.CreateContext;

        var code   = accountNumber[..3];
        var branch = accountNumber[3..7];
        var number = accountNumber[7..16];
        var type   = accountNumber[16..];

        return await context.Accounts.IncludeAll()
                            .FirstOrDefaultAsync(account => account.Client!.Bank!.Code == code && account.Number == number && account.Type!.Code == type);
    }

    public async Task<Account> Add(Account account)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedAccount = await context.Accounts.AddAsync(account);

        await context.SaveChangesAsync();

        return addedAccount.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<Account> accounts)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(accounts, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<Account> Update(Account account)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.Accounts.Where(dbAccount => dbAccount.Id == account.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbAccount => dbAccount.Status, account.Status)
                                                           .SetProperty(dbAccount => dbAccount.Name,         account.Name)
                                                           .SetProperty(dbAccount => dbAccount.DailyLimit,   account.DailyLimit)
                                                           .SetProperty(dbAccount => dbAccount.MonthlyLimit, account.MonthlyLimit)
                                                           .SetProperty(dbAccount => dbAccount.ModifiedAt,   account.ModifiedAt));

        return account;
    }

    #region Balance & Available Balance

    public async Task<bool> IncreaseBalance(Guid accountId, decimal amount, ApplicationContext? context = null)
    {
        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        result += await context.Accounts.Where(account => account.Id == accountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance                   + amount)
                                                                     .SetProperty(account => account.AvailableBalance, account => account.AvailableBalance + amount)
                                                                     .SetProperty(account => account.ModifiedAt,       DateTime.UtcNow));

        result += await context.AccountCurrencies.Where(account => account.Id == accountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance                   + amount)
                                                                     .SetProperty(account => account.AvailableBalance, account => account.AvailableBalance + amount)
                                                                     .SetProperty(account => account.ModifiedAt,       DateTime.UtcNow));

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 1;
    }

    public async Task<bool> DecreaseBalance(Guid accountId, decimal amount, ApplicationContext? context = null)
    {
        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        result += await context.Accounts.Where(account => account.Id == accountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance - amount)
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        result += await context.AccountCurrencies.Where(account => account.Id == accountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance - amount)
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 1;
    }

    public async Task<bool> DecreaseBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext? context)
    {
        if (!Data.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId))
            return false;

        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        if (isContextProvided is false)
            await context.Database.BeginTransactionAsync();

        result += await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance,
                                                                                  account => account.Balance - (account.Id == accountId ? accountAmount : bankAccountAmount))
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        result += await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance,
                                                                                  account => account.Balance - (account.Id == accountId ? accountAmount : bankAccountAmount))
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        if (isContextProvided is false && result == 2)
            await context.Database.CommitTransactionAsync();

        if (isContextProvided is false && result != 2)
            await context.Database.RollbackTransactionAsync();

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 2;
    }

    public async Task<bool> DecreaseAvailableBalance(Guid accountId, decimal amount, ApplicationContext? context = null)
    {
        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        result += await context.Accounts.Where(account => account.Id               == accountId)
                               .Where(account => account.AvailableBalance - amount >= 0)
                               .GroupJoin(context.Transactions, account => account.Id, transaction => transaction.FromAccountId,
                                          (account, transaction) => new { account, transaction })
                               .Select(group => new
                                                {
                                                    Entity = group.account,
                                                    Daily = group.transaction.Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                   transaction.CreatedAt      >= Midnight                   &&
                                                                                                   transaction.Status         != TransactionStatus.Canceled &&
                                                                                                   transaction.Status         != TransactionStatus.Failed)
                                                                 .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m,
                                                    Monthly = group.transaction.Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                     transaction.CreatedAt      >= FirstDayOfMonth            &&
                                                                                                     transaction.Status         != TransactionStatus.Canceled &&
                                                                                                     transaction.Status         != TransactionStatus.Failed)
                                                                   .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m
                                                })
                               .Where(spending => spending.Daily + amount <= spending.Entity.DailyLimit && spending.Monthly + amount <= spending.Entity.MonthlyLimit)
                               .Select(spending => spending.Entity)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance, account => account.AvailableBalance - amount)
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        result += await context.AccountCurrencies.Where(account => account.Id      == accountId)
                               .Where(account => account.AvailableBalance - amount >= 0)
                               .GroupJoin(context.Transactions, account => account.Id, transaction => transaction.FromAccountId,
                                          (account, transaction) => new { account, transaction })
                               .Select(group => new
                                                {
                                                    Entity = group.account,
                                                    Daily = group.transaction.Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                   transaction.CreatedAt      >= Midnight                   &&
                                                                                                   transaction.Status         != TransactionStatus.Canceled &&
                                                                                                   transaction.Status         != TransactionStatus.Failed)
                                                                 .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m,
                                                    Monthly = group.transaction.Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                     transaction.CreatedAt      >= FirstDayOfMonth            &&
                                                                                                     transaction.Status         != TransactionStatus.Canceled &&
                                                                                                     transaction.Status         != TransactionStatus.Failed)
                                                                   .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m
                                                })
                               .Where(spending => spending.Daily + amount <= spending.Entity.DailyLimit && spending.Monthly + amount <= spending.Entity.MonthlyLimit)
                               .Select(spending => spending.Entity)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance, account => account.AvailableBalance - amount)
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 1;
    }

    public async Task<bool> DecreaseAvailableBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext? context)
    {
        if (!Data.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId))
            return false;

        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        if (isContextProvided is false)
            await context.Database.BeginTransactionAsync();

        result += await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .Where(account => account.AvailableBalance - (account.Id == accountId ? accountAmount : bankAccountAmount) >= 0)
                               .GroupJoin(context.Transactions, account => account.Id, transaction => transaction.FromAccountId,
                                          (account, transaction) => new { account, transaction })
                               .Select(group => new
                                                {
                                                    Entity = group.account,
                                                    Daily = group.transaction.Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                   transaction.CreatedAt      >= Midnight                   &&
                                                                                                   transaction.Status         != TransactionStatus.Canceled &&
                                                                                                   transaction.Status         != TransactionStatus.Failed)
                                                                 .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m,
                                                    Monthly = group.transaction.Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                     transaction.CreatedAt      >= FirstDayOfMonth            &&
                                                                                                     transaction.Status         != TransactionStatus.Canceled &&
                                                                                                     transaction.Status         != TransactionStatus.Failed)
                                                                   .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m
                                                })
                               .Where(spending => spending.Daily   + (spending.Entity.Id == accountId ? accountAmount : bankAccountAmount) <= spending.Entity.DailyLimit &&
                                                  spending.Monthly + (spending.Entity.Id == accountId ? accountAmount : bankAccountAmount) <= spending.Entity.MonthlyLimit)
                               .Select(spending => spending.Entity)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance,
                                                                                  account => account.AvailableBalance -
                                                                                             (account.Id == accountId ? accountAmount : bankAccountAmount)));

        result += await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .Where(account => account.AvailableBalance - (account.Id == accountId ? accountAmount : bankAccountAmount) >= 0)
                               .GroupJoin(context.Transactions, account => account.Id, transaction => transaction.FromAccountId,
                                          (account, transaction) => new { account, transaction })
                               .Select(group => new
                                                {
                                                    Entity = group.account,
                                                    Daily = group.transaction.Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                   transaction.CreatedAt      >= Midnight                   &&
                                                                                                   transaction.Status         != TransactionStatus.Canceled &&
                                                                                                   transaction.Status         != TransactionStatus.Failed)
                                                                 .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m,
                                                    Monthly = group.transaction.Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                     transaction.CreatedAt      >= FirstDayOfMonth            &&
                                                                                                     transaction.Status         != TransactionStatus.Canceled &&
                                                                                                     transaction.Status         != TransactionStatus.Failed)
                                                                   .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m
                                                })
                               .Where(spending => spending.Daily   + (spending.Entity.Id == accountId ? accountAmount : bankAccountAmount) <= spending.Entity.DailyLimit &&
                                                  spending.Monthly + (spending.Entity.Id == accountId ? accountAmount : bankAccountAmount) <= spending.Entity.MonthlyLimit)
                               .Select(spending => spending.Entity)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance,
                                                                                  account => account.AvailableBalance -
                                                                                             (account.Id == accountId ? accountAmount : bankAccountAmount))
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        if (isContextProvided is false && result == 2)
            await context.Database.CommitTransactionAsync();

        if (isContextProvided is false && result != 2)
            await context.Database.RollbackTransactionAsync();

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 2;
    }

    public async Task<bool> DecreaseAvailableBalanceWithoutLimitCheck(Guid accountId, decimal amount, ApplicationContext? context = null)
    {
        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        result += await context.Accounts.Where(account => account.Id               == accountId)
                               .Where(account => account.AvailableBalance - amount >= 0)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance, account => account.AvailableBalance - amount)
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        result += await context.AccountCurrencies.Where(account => account.Id == accountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance, account => account.AvailableBalance - amount)
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 1;
    }

    public async Task<bool> DecreaseAvailableBalanceWithoutLimitCheck(Guid                accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount,
                                                                      ApplicationContext? context)
    {
        if (!Data.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId))
            return false;

        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        result += await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .Where(account => account.AvailableBalance - (account.Id == accountId ? accountAmount : bankAccountAmount) >= 0)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance,
                                                                                  account => account.AvailableBalance -
                                                                                             (account.Id == accountId ? accountAmount : bankAccountAmount))
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        result += await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance,
                                                                                  account => account.AvailableBalance -
                                                                                             (account.Id == accountId ? accountAmount : bankAccountAmount))
                                                                     .SetProperty(account => account.ModifiedAt, DateTime.UtcNow));

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 2;
    }

    public async Task<bool> DecreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount, ApplicationContext? context)
    {
        if (!Data.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId))
            return false;

        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        if (isContextProvided is false)
            await context.Database.BeginTransactionAsync();

        result += await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance                   - amount)
                                                                     .SetProperty(account => account.AvailableBalance, account => account.AvailableBalance - amount)
                                                                     .SetProperty(account => account.ModifiedAt,       DateTime.UtcNow));

        result += await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance                   - amount)
                                                                     .SetProperty(account => account.AvailableBalance, account => account.AvailableBalance - amount)
                                                                     .SetProperty(account => account.ModifiedAt,       DateTime.UtcNow));

        if (isContextProvided is false && result == 2)
            await context.Database.CommitTransactionAsync();

        if (isContextProvided is false && result != 2)
            await context.Database.RollbackTransactionAsync();

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 2;
    }

    public async Task<bool> IncreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount, ApplicationContext? context)
    {
        if (!Data.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId))
            return false;

        var result            = 0;
        var isContextProvided = context is not null;
        context ??= await m_ContextFactory.CreateContext;

        if (isContextProvided is false)
            await context.Database.BeginTransactionAsync();

        result += await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance                   + amount)
                                                                     .SetProperty(account => account.AvailableBalance, account => account.AvailableBalance + amount)
                                                                     .SetProperty(account => account.ModifiedAt,       DateTime.UtcNow));

        result += await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                               .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance                   + amount)
                                                                     .SetProperty(account => account.AvailableBalance, account => account.AvailableBalance + amount)
                                                                     .SetProperty(account => account.ModifiedAt,       DateTime.UtcNow));

        if (isContextProvided is false && result == 2)
            await context.Database.CommitTransactionAsync();

        if (isContextProvided is false && result != 2)
            await context.Database.RollbackTransactionAsync();

        if (isContextProvided is false)
            await context.DisposeAsync();

        return result == 2;
    }

    #endregion
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Account, object?> IncludeAll(this DbSet<Account> set)
    {
        return set.Include(account => account.Client)
                  .ThenIncludeAll(account => account.Client, nameof(User.Accounts))
                  .Include(account => account.Employee)
                  .ThenIncludeAll(account => account.Employee, nameof(User.Accounts))
                  .Include(account => account.Currency)
                  .ThenIncludeAll(account => account.Currency)
                  .Include(account => account.AccountCurrencies)
                  .ThenIncludeAll(account => account.AccountCurrencies, nameof(AccountCurrency.Account))
                  .Include(account => account.Type)
                  .ThenIncludeAll(account => account.Type);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Account?> value,
                                                                                 Expression<Func<TEntity, Account?>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Account.Client)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account!.Client);

        if (!excludeProperties.Contains(nameof(Account.Employee)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account!.Employee);

        if (!excludeProperties.Contains(nameof(Account.Currency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account!.Currency);

        if (!excludeProperties.Contains(nameof(Account.Type)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account!.Type);

        if (!excludeProperties.Contains(nameof(Account.AccountCurrencies)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account!.AccountCurrencies);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Account>> value,
                                                                                 Expression<Func<TEntity, List<Account>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Account.Client)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account.Client);

        if (!excludeProperties.Contains(nameof(Account.Employee)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account.Employee);

        if (!excludeProperties.Contains(nameof(Account.Currency)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account.Currency);

        if (!excludeProperties.Contains(nameof(Account.Type)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account.Type);

        if (!excludeProperties.Contains(nameof(Account.AccountCurrencies)))
            query = query.Include(navigationExpression)
                         .ThenInclude(account => account.AccountCurrencies);

        return query;
    }
}

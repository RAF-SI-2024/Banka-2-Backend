using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.UserService.BackgroundServices;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IAccountRepository
{
    Task<Page<Account>> FindAll(AccountFilterQuery accountFilterQuery, Pageable pageable);

    Task<Page<Account>> FindAllByClientId(Guid clientId, Pageable pageable);

    Task<Account?> FindById(Guid id);

    Task<bool> Exists(Guid id);

    Task<Account?> FindByNumber(string number);

    Task<Account> Add(Account account);

    Task<Account> Update(Account account);

    Task<bool> DecreaseBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount);

    Task<bool> DecreaseAvailableBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount);

    Task<bool> DecreaseAvailableBalanceWithoutLimitCheck(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount);

    Task<bool> IncreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount);

    Task<bool> DecreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount);

    Task<bool> DecreaseBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext context);

    Task<bool> DecreaseAvailableBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext context);

    Task<bool> DecreaseAvailableBalanceWithoutLimitCheck(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext context);

    Task<bool> IncreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount, ApplicationContext context);

    Task<bool> DecreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount, ApplicationContext context);
}

public class AccountRepository(IDbContextFactory<ApplicationContext> contextFactory, ApplicationContext context, TransactionBackgroundService transactionBackgroundService)
: IAccountRepository
{
    private readonly ApplicationContext                    m_Context                      = context;
    private readonly IDbContextFactory<ApplicationContext> m_ContextFactory               = contextFactory;
    private readonly TransactionBackgroundService          m_TransactionBackgroundService = transactionBackgroundService;

    private Task<ApplicationContext> CreateContext => m_ContextFactory.CreateDbContextAsync();

    private static DateTime Midnight => DateTime.UtcNow.Date;

    private static DateTime FirstDayOfMonth =>
    DateTime.UtcNow.AddDays(1 - DateTime.UtcNow.Day)
            .Date;

    public async Task<Page<Account>> FindAll(AccountFilterQuery accountFilterQuery, Pageable pageable)
    {
        var accountQuery = m_Context.Accounts.IncludeAll()
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
        var accounts = await m_Context.Accounts.IncludeAll()
                                      .Where(account => account.ClientId == clientId)
                                      .Skip((pageable.Page - 1) * pageable.Size)
                                      .Take(pageable.Size)
                                      .ToListAsync();

        var totalElements = await m_Context.Accounts.CountAsync();

        return new Page<Account>(accounts, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Account?> FindById(Guid id)
    {
        await using var context = await CreateContext;

        Console.WriteLine($"FindById | {DateTime.Now:hh:mm:ss.fff}");
        await Task.Delay(100);

        return await context.Accounts.IncludeAll()
                            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<bool> Exists(Guid id)
    {
        return await m_Context.Accounts.AnyAsync(account => account.Id == id);
    }

    public async Task<Account?> FindByNumber(string number)
    {
        await using var context = await CreateContext;

        return await context.Accounts.IncludeAll()
                            .FirstOrDefaultAsync(a => a.Number == number);
    }

    public async Task<Account> Add(Account account)
    {
        var addedAccount = await m_Context.Accounts.AddAsync(account);

        await m_Context.SaveChangesAsync();

        return addedAccount.Entity;
    }

    public async Task<Account> Update(Account account)
    {
        await m_Context.Accounts.Where(dbAccount => dbAccount.Id == account.Id)
                       .ExecuteUpdateAsync(setters => setters.SetProperty(dbAccount => dbAccount.Status, account.Status)
                                                             .SetProperty(dbAccount => dbAccount.Name,         account.Name)
                                                             .SetProperty(dbAccount => dbAccount.DailyLimit,   account.DailyLimit)
                                                             .SetProperty(dbAccount => dbAccount.MonthlyLimit, account.MonthlyLimit)
                                                             .SetProperty(dbAccount => dbAccount.ModifiedAt,   account.ModifiedAt));

        return account;
    }

    public async Task<bool> DecreaseBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount)
    {
        await using var context = await CreateContext;

        return await DecreaseBalance(accountId, bankCurrencyId, accountAmount, bankAccountAmount, context);
    }

    public async Task<bool> DecreaseAvailableBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount)
    {
        await using var context = await CreateContext;

        return await DecreaseAvailableBalance(accountId, bankCurrencyId, accountAmount, bankAccountAmount, context);
    }

    public async Task<bool> DecreaseAvailableBalanceWithoutLimitCheck(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount)
    {
        await using var context = await CreateContext;

        return await DecreaseAvailableBalanceWithoutLimitCheck(accountId, bankCurrencyId, accountAmount, bankAccountAmount, context);
    }

    public async Task<bool> IncreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount)
    {
        await using var context = await CreateContext;

        return await IncreaseBalances(accountId, bankCurrencyId, amount, context);
    }

    public async Task<bool> DecreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount)
    {
        await using var context = await CreateContext;

        return await DecreaseBalances(accountId, bankCurrencyId, amount, context);
    }

    public async Task<bool> DecreaseBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext context)
    {
        m_TransactionBackgroundService.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId);

        var updatedAccounts = await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                           .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance,
                                                                                              account => account.Balance - (account.Id == accountId
                                                                                                                            ? accountAmount
                                                                                                                            : bankAccountAmount)));

        var updatedAccountCurrencies = await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                                    .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance,
                                                                                                       account => account.Balance - (account.Id == accountId
                                                                                                                                     ? accountAmount
                                                                                                                                     : bankAccountAmount)));

        return updatedAccounts + updatedAccountCurrencies == 2;
    }

    public async Task<bool> DecreaseAvailableBalance(Guid accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount, ApplicationContext context)
    {
        m_TransactionBackgroundService.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId);

        var updatedAccounts = await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                           .Where(account => account.AvailableBalance - (account.Id == accountId ? accountAmount : bankAccountAmount) >= 0)
                                           .GroupJoin(context.Transactions, account => account.Id, transaction => transaction.FromAccountId,
                                                      (account, transaction) => new { account, transaction })
                                           .Select(group => new
                                                            {
                                                                Entity = group.account,
                                                                Daily = group.transaction
                                                                             .Where(transaction => transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                                   transaction.CreatedAt      >= Midnight                   &&
                                                                                                   transaction.Status         != TransactionStatus.Canceled &&
                                                                                                   transaction.Status         != TransactionStatus.Failed)
                                                                             .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m,
                                                                Monthly = group.transaction
                                                                               .Where(transaction =>
                                                                                      transaction.FromCurrencyId == group.account.CurrencyId   &&
                                                                                      transaction.CreatedAt      >= FirstDayOfMonth            &&
                                                                                      transaction.Status         != TransactionStatus.Canceled &&
                                                                                      transaction.Status         != TransactionStatus.Failed)
                                                                               .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m
                                                            })
                                           .Where(spending =>
                                                  spending.Daily   + (spending.Entity.Id == accountId ? accountAmount : bankAccountAmount) <= spending.Entity.DailyLimit &&
                                                  spending.Monthly + (spending.Entity.Id == accountId ? accountAmount : bankAccountAmount) <= spending.Entity.MonthlyLimit)
                                           .Select(spending => spending.Entity)
                                           .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance,
                                                                                              account => account.AvailableBalance -
                                                                                                         (account.Id == accountId ? accountAmount : bankAccountAmount)));

        var updatedAccountCurrencies = await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                                    .Where(account => account.AvailableBalance - (account.Id == accountId ? accountAmount : bankAccountAmount) >= 0)
                                                    .GroupJoin(context.Transactions, account => account.Id, transaction => transaction.FromAccountId,
                                                               (account, transaction) => new { account, transaction })
                                                    .Select(group => new
                                                                     {
                                                                         Entity = group.account,
                                                                         Daily = group.transaction
                                                                                      .Where(transaction =>
                                                                                             transaction.FromCurrencyId ==
                                                                                             group.account.CurrencyId                            &&
                                                                                             transaction.CreatedAt >= Midnight                   &&
                                                                                             transaction.Status    != TransactionStatus.Canceled &&
                                                                                             transaction.Status    != TransactionStatus.Failed)
                                                                                      .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m,
                                                                         Monthly = group.transaction
                                                                                        .Where(transaction =>
                                                                                               transaction.FromCurrencyId ==
                                                                                               group.account.CurrencyId                            &&
                                                                                               transaction.CreatedAt >= FirstDayOfMonth            &&
                                                                                               transaction.Status    != TransactionStatus.Canceled &&
                                                                                               transaction.Status    != TransactionStatus.Failed)
                                                                                        .Sum(transaction => (decimal?)transaction.FromAmount) ?? 0m
                                                                     })
                                                    .Where(spending => spending.Daily + (spending.Entity.Id == accountId ? accountAmount : bankAccountAmount) <=
                                                                       spending.Entity.DailyLimit &&
                                                                       spending.Monthly + (spending.Entity.Id == accountId ? accountAmount : bankAccountAmount) <=
                                                                       spending.Entity.MonthlyLimit)
                                                    .Select(spending => spending.Entity)
                                                    .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance,
                                                                                                       account => account.AvailableBalance -
                                                                                                                  (account.Id == accountId ? accountAmount : bankAccountAmount)));

        return updatedAccounts + updatedAccountCurrencies == 2;
    }

    public async Task<bool> DecreaseAvailableBalanceWithoutLimitCheck(Guid               accountId, Guid bankCurrencyId, decimal accountAmount, decimal bankAccountAmount,
                                                                      ApplicationContext context)
    {
        m_TransactionBackgroundService.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId);

        var updatedAccounts = await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                           .Where(account => account.AvailableBalance - (account.Id == accountId ? accountAmount : bankAccountAmount) >= 0)
                                           .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance,
                                                                                              account => account.AvailableBalance -
                                                                                                         (account.Id == accountId ? accountAmount : bankAccountAmount)));

        var updatedAccountCurrencies = await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                                    .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.AvailableBalance,
                                                                                                       account => account.AvailableBalance -
                                                                                                                  (account.Id == accountId ? accountAmount : bankAccountAmount)));

        return updatedAccounts + updatedAccountCurrencies == 2;
    }

    public async Task<bool> DecreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount, ApplicationContext context)
    {
        m_TransactionBackgroundService.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId);

        var updatedAccounts = await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                           .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance                   - amount)
                                                                                 .SetProperty(account => account.AvailableBalance, account => account.AvailableBalance - amount));

        var updatedAccountCurrencies = await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                                    .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance - amount)
                                                                                          .SetProperty(account => account.AvailableBalance,
                                                                                                       account => account.AvailableBalance - amount));

        return updatedAccounts + updatedAccountCurrencies == 2;
    }

    public async Task<bool> IncreaseBalances(Guid accountId, Guid bankCurrencyId, decimal amount, ApplicationContext context)
    {
        m_TransactionBackgroundService.BankAccount.TryFindAccount(bankCurrencyId, out var bankAccountId);

        var updatedAccounts = await context.Accounts.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                           .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance                   + amount)
                                                                                 .SetProperty(account => account.AvailableBalance, account => account.AvailableBalance + amount));

        var updatedAccountCurrencies = await context.AccountCurrencies.Where(account => account.Id == accountId || account.Id == bankAccountId)
                                                    .ExecuteUpdateAsync(setters => setters.SetProperty(account => account.Balance, account => account.Balance + amount)
                                                                                          .SetProperty(account => account.AvailableBalance,
                                                                                                       account => account.AvailableBalance + amount));

        return updatedAccounts + updatedAccountCurrencies == 2;
    }
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
                                                                                 Expression<Func<TEntity, Account?>>          navigationExpression, params string[] excludeProperties)
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

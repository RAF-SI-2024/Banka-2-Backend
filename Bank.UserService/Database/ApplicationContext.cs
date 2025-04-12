using Bank.UserService.Database.EntityConfigurations;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Database;

using BankModel = Models.Bank;

public class ApplicationContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<BankModel>           Banks                { init; get; }
    public DbSet<User>                Users                { init; get; }
    public DbSet<Account>             Accounts             { init; get; }
    public DbSet<AccountType>         AccountTypes         { init; get; }
    public DbSet<AccountCurrency>     AccountCurrencies    { init; get; }
    public DbSet<Country>             Countries            { init; get; }
    public DbSet<Currency>            Currencies           { init; get; }
    public DbSet<CardType>            CardTypes            { init; get; }
    public DbSet<Card>                Cards                { init; get; }
    public DbSet<Company>             Companies            { init; get; }
    public DbSet<Exchange>            Exchanges            { init; get; }
    public DbSet<TransactionCode>     TransactionCodes     { init; get; }
    public DbSet<TransactionTemplate> TransactionTemplates { init; get; }
    public DbSet<Transaction>         Transactions         { init; get; }
    public DbSet<Loan>                Loans                { init; get; }
    public DbSet<Installment>         Installments         { init; get; }
    public DbSet<LoanType>            LoanTypes            { init; get; }
    public DbSet<Order>     Orders               { init; get; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new BankEntityConfiguration());
        builder.ApplyConfiguration(new UserEntityConfiguration());
        builder.ApplyConfiguration(new AccountEntityConfiguration());
        builder.ApplyConfiguration(new AccountTypeEntityConfiguration());
        builder.ApplyConfiguration(new AccountCurrencyEntityConfiguration());
        builder.ApplyConfiguration(new CardTypeEntityConfiguration());
        builder.ApplyConfiguration(new CardEntityConfiguration());
        builder.ApplyConfiguration(new CountryEntityConfiguration());
        builder.ApplyConfiguration(new CurrencyEntityConfiguration());
        builder.ApplyConfiguration(new CompanyEntityConfiguration());
        builder.ApplyConfiguration(new LoanEntityConfiguration());
        builder.ApplyConfiguration(new InstallmentEntityConfiguration());
        builder.ApplyConfiguration(new LoanTypeEntityConfiguration());
        builder.ApplyConfiguration(new TransactionCodeEntityConfiguration());
        builder.ApplyConfiguration(new TransactionTemplateEntityConfiguration());
        builder.ApplyConfiguration(new TransactionEntityConfiguration());
        builder.ApplyConfiguration(new ExchangeEntityConfiguration());
        builder.ApplyConfiguration(new OrderEntityConfiguration());
    }
}

using Bank.Database.Core;
using Bank.UserService.Configurations;
using Bank.UserService.Database;
using Bank.UserService.Repositories;
using Bank.UserService.Services;

namespace Bank.UserService.BackgroundServices;

public class DatabaseBackgroundService(
    IDatabaseContextFactory<ApplicationContext> contextFactory,
    IAccountCurrencyRepository                  accountCurrencyRepository,
    IAccountRepository                          accountRepository,
    IAccountTypeRepository                      accountTypeRepository,
    IBankRepository                             bankRepository,
    ICardRepository                             cardRepository,
    ICardTypeRepository                         cardTypeRepository,
    ICompanyRepository                          companyRepository,
    ICountryRepository                          countryRepository,
    ICurrencyRepository                         currencyRepository,
    IExchangeRepository                         exchangeRepository,
    IInstallmentRepository                      installmentRepository,
    ILoanRepository                             loanRepository,
    ILoanTypeRepository                         loanTypeRepository,
    ITransactionCodeRepository                  transactionCodeRepository,
    ITransactionRepository                      transactionRepository,
    ITransactionTemplateRepository              transactionTemplateRepository,
    IUserRepository                             userRepository,
    IDataService                                dataService
)
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory                = contextFactory;
    private readonly IAccountCurrencyRepository                  m_AccountCurrencyRepository     = accountCurrencyRepository;
    private readonly IAccountRepository                          m_AccountRepository             = accountRepository;
    private readonly IAccountTypeRepository                      m_AccountTypeRepository         = accountTypeRepository;
    private readonly IBankRepository                             m_BankRepository                = bankRepository;
    private readonly ICardRepository                             m_CardRepository                = cardRepository;
    private readonly ICardTypeRepository                         m_CardTypeRepository            = cardTypeRepository;
    private readonly ICompanyRepository                          m_CompanyRepository             = companyRepository;
    private readonly ICountryRepository                          m_CountryRepository             = countryRepository;
    private readonly ICurrencyRepository                         m_CurrencyRepository            = currencyRepository;
    private readonly IExchangeRepository                         m_ExchangeRepository            = exchangeRepository;
    private readonly IInstallmentRepository                      m_InstallmentRepository         = installmentRepository;
    private readonly ILoanRepository                             m_LoanRepository                = loanRepository;
    private readonly ILoanTypeRepository                         m_LoanTypeRepository            = loanTypeRepository;
    private readonly ITransactionCodeRepository                  m_TransactionCodeRepository     = transactionCodeRepository;
    private readonly ITransactionRepository                      m_TransactionRepository         = transactionRepository;
    private readonly ITransactionTemplateRepository              m_TransactionTemplateRepository = transactionTemplateRepository;
    private readonly IUserRepository                             m_UserRepository                = userRepository;
    private readonly IDataService                                m_DataService                   = dataService;

    public async Task OnApplicationStarted(CancellationToken cancellationToken)
    {
        await using (var context = await m_ContextFactory.CreateContext)
        {
            if (Configuration.Database.CreateDrop)
                await context.Database.EnsureDeletedAsync(cancellationToken);

            await context.Database.EnsureCreatedAsync(cancellationToken);
        }

        // @formatter:off
        
        var banksTask            = m_DataService.GetBanks();
        var accountTypesTask     = m_DataService.GetAccountTypes();
        var cardTypesTask        = m_DataService.GetCardTypes();
        var currenciesTask       = m_DataService.GetCurrencies();
        var loanTypesTask        = m_DataService.GetLoanTypes();
        var transactionCodesTask = m_DataService.GetTransactionCodes();

        await Task.WhenAll(banksTask, accountTypesTask, cardTypesTask, currenciesTask, loanTypesTask, transactionCodesTask);
        
        var banks            = await banksTask;
        var accountTypes     = await accountTypesTask;
        var cardTypes        = await cardTypesTask;
        var currencies       = await currenciesTask;
        var loanTypes        = await loanTypesTask;
        var transactionCodes = await transactionCodesTask;
        
        await Task.WhenAll(m_BankRepository.AddRange(banks.upToDate ? [] : banks.values),
                           m_AccountTypeRepository.AddRange(accountTypes.upToDate ? [] : accountTypes.values),
                           m_CardTypeRepository.AddRange(cardTypes.upToDate ? [] : cardTypes.values),
                           m_CurrencyRepository.AddRange(currencies.upToDate ? [] : currencies.values),
                           m_LoanTypeRepository.AddRange(loanTypes.upToDate ? [] : loanTypes.values),
                           m_TransactionCodeRepository.AddRange(transactionCodes.upToDate ? [] : transactionCodes.values));

        var usersTask             = m_DataService.GetUsers();
        var countriesTask         = m_DataService.GetCountries();
        var currencyExchangesTask = m_DataService.GetCurrencyExchanges();

        await Task.WhenAll(usersTask, currenciesTask, currenciesTask);
        
        var users             = await usersTask;
        var countries         = await countriesTask;
        var currencyExchanges = await currencyExchangesTask;
        
        await Task.WhenAll(m_UserRepository.AddRange(users.upToDate ? [] : users.values),
                           m_CountryRepository.AddRange(countries.upToDate ? [] : countries.values),
                           m_ExchangeRepository.AddRange(currencyExchanges.upToDate ? [] : currencyExchanges.values));
        
        var accountsTask             = m_DataService.GetAccounts();
        var companiesTask            = m_DataService.GetCompanies();
        var loansTask                = m_DataService.GetLoans();
        var transactionTemplatesTask = m_DataService.GetTransactionTemplates();

        await Task.WhenAll(accountsTask, companiesTask, loansTask, transactionTemplatesTask);
        
        var accounts             = await accountsTask;
        var companies            = await companiesTask;
        var loans                = await loansTask;
        var transactionTemplates = await transactionTemplatesTask;
        
        await Task.WhenAll(m_AccountRepository.AddRange(accounts.upToDate ? [] : accounts.values),
                           m_CompanyRepository.AddRange(companies.upToDate ? [] : companies.values),
                           m_LoanRepository.AddRange(loans.upToDate ? [] : loans.values),
                           m_TransactionTemplateRepository.AddRange(transactionTemplates.upToDate ? [] : transactionTemplates.values));

        var accountCurrenciesTask = m_DataService.GetAccountCurrencies();
        var cardsTask             = m_DataService.GetCards();
        var installmentsTask      = m_DataService.GetInstallments();
        var transactionsTask      = m_DataService.GetTransactions();
        
        await Task.WhenAll(accountCurrenciesTask, cardsTask, installmentsTask, transactionsTask);

        var accountCurrencies = await accountCurrenciesTask;
        var cards             = await cardsTask;
        var installments      = await installmentsTask;
        var transactions      = await transactionsTask;

        await Task.WhenAll(m_AccountCurrencyRepository.AddRange(accountCurrencies.upToDate ? [] : accountCurrencies.values),
                           m_CardRepository.AddRange(cards.upToDate ? [] : cards.values),
                           m_InstallmentRepository.AddRange(installments.upToDate ? [] : installments.values),
                           m_TransactionRepository.AddRange(transactions.upToDate ? [] : transactions.values));
        
        // @formatter:on

        await m_DataService.Instantiate();
    }

    public Task OnApplicationStopped(CancellationToken _) => Task.CompletedTask;
}

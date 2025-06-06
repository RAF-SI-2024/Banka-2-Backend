using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.Http.Clients.User;

public interface IUserServiceHttpClient
{
    #region AccountController methods

    Task<Page<AccountResponse>> GetAllAccounts(AccountFilterQuery filter, Pageable pageable);

    Task<Page<AccountResponse>> GetAllAccountsForClient(Guid clientId, AccountFilterQuery filter, Pageable pageable);

    Task<Page<CardResponse>> GetAllCardsForClient(Guid clientId);

    Task<AccountResponse?> GetOneAccount(Guid accountId);

    Task<AccountResponse?> CreateAccount(AccountCreateRequest createRequest);

    Task<AccountResponse?> UpdateAccount(Guid accountId, AccountUpdateClientRequest updateRequest);

    Task<AccountResponse?> UpdateAccount(Guid accountId, AccountUpdateEmployeeRequest updateRequest);

    #endregion

    #region AccountCurrencyController methods

    Task<Page<AccountCurrencyResponse>> GetAllAccountCurrencies(Pageable pageable);

    Task<AccountCurrencyResponse?> GetOneAccountCurrency(Guid accountCurrencyId);

    Task<AccountCurrencyResponse?> CreateAccountCurrency(AccountCurrencyCreateRequest createRequest);

    Task<AccountCurrencyResponse?> UpdateAccountCurrency(Guid accountCurrencyId, AccountCurrencyClientUpdateRequest updateRequest);

    #endregion

    #region AccountTypeController methods

    Task<Page<AccountTypeResponse>> GetAllAccountTypes(AccountTypeFilterQuery filter, Pageable pageable);

    Task<AccountTypeResponse?> GetOneAccountType(Guid accountTypeId);

    #endregion

    #region CardController methods

    Task<Page<CardResponse>> GetAllCards(CardFilterQuery filter, Pageable pageable);

    Task<Page<CardResponse>> GetAllCardsForAccount(Guid accountId, CardFilterQuery filter, Pageable pageable);

    Task<CardResponse?> GetOneCard(Guid cardId);

    Task<CardResponse?> CreateCard(CardCreateRequest createRequest);

    Task<CardResponse?> UpdateCard(Guid cardId, CardUpdateStatusRequest updateRequest);

    Task<CardResponse?> UpdateCard(Guid cardId, CardUpdateLimitRequest updateRequest);

    #endregion

    #region CardTypeController methods

    Task<Page<CardTypeResponse>> GetAllCardTypes(CardTypeFilterQuery filter, Pageable pageable);

    Task<CardTypeResponse> GetOneCardType(Guid cardTypeId);

    #endregion

    #region ClientController methods

    Task<Page<ClientResponse>> GetAllClients(UserFilterQuery filter, Pageable pageable);

    Task<ClientResponse?> GetOneClient(Guid clientId);

    Task<ClientResponse?> CreateClient(ClientCreateRequest createRequest);

    Task<ClientResponse?> UpdateClient(Guid clientId, ClientUpdateRequest updateRequest);

    #endregion

    #region CompanyController methods

    Task<Page<CompanyResponse>> GetAllCompanies(CompanyFilterQuery filter, Pageable pageable);

    Task<CompanyResponse?> GetOneCompany(Guid companyId);

    Task<CompanyResponse?> CreateCompany(CompanyCreateRequest createRequest);

    Task<CompanyResponse?> UpdateCompany(Guid companyId, CompanyUpdateRequest updateRequest);

    #endregion

    #region CountryController methods

    Task<Page<CountryResponse>> GetAllCountries(CountryFilterQuery filter, Pageable pageable);

    Task<CountryResponse?> GetOneCountry(Guid countryId);

    #endregion

    #region CurrencyController methods

    Task<List<CurrencyResponse>> GetAllCurrencies(CurrencyFilterQuery filter);

    Task<List<CurrencySimpleResponse>> GetAllSimpleCurrencies(CurrencyFilterQuery filter);

    Task<CurrencyResponse?> GetOneCurrency(Guid currencyId);

    Task<CurrencySimpleResponse?> GetOneSimpleCurrency(Guid currencyId);

    #endregion

    #region EmployeeController methods

    Task<Page<EmployeeResponse>> GetAllEmployees(UserFilterQuery filter, Pageable pageable);

    Task<EmployeeResponse?> GetOneEmployee(Guid employeeId);

    Task<EmployeeResponse?> CreateEmployee(EmployeeCreateRequest createRequest);

    Task<EmployeeResponse?> UpdateEmployee(Guid employeeId, EmployeeUpdateRequest updateRequest);

    #endregion

    #region ExchangeController methods

    Task<List<ExchangeResponse>> GetAllExchanges(ExchangeFilterQuery filter);

    Task<ExchangeResponse?> GetOneExchange(Guid exchangeId);

    Task<ExchangeResponse?> GetExchangeByCurrencies(ExchangeBetweenQuery query);

    Task<ExchangeResponse?> MakeExchange(ExchangeMakeExchangeRequest makeExchangeRequest);

    Task<ExchangeResponse?> UpdateExchange(Guid exchangeId, ExchangeUpdateRequest updateRequest);

    #endregion

    #region InstallmentController methods

    Task<Page<InstallmentResponse>> GetAllInstallmentsForLoan(Guid loanId, Pageable pageable);

    Task<InstallmentResponse?> GetOneInstallment(Guid installmentId);

    Task<InstallmentResponse?> CreateInstallment(InstallmentCreateRequest createRequest);

    Task<InstallmentResponse?> UpdateInstallment(Guid installmentId, InstallmentUpdateRequest updateRequest);

    #endregion

    #region LoanController methods

    Task<Page<LoanResponse>> GetAllLoans(LoanFilterQuery loanFilterQuery, Pageable pageable);

    Task<Page<LoanResponse>> GetAllLoansForClient(Guid clientId, Pageable pageable);

    Task<LoanResponse?> GetOneLoan(Guid loanId);

    Task<LoanResponse?> CreateLoan(LoanCreateRequest createRequest);

    Task<LoanResponse?> UpdateLoan(Guid loanId, LoanUpdateRequest updateRequest);

    #endregion

    #region LoanTypeController methods

    Task<Page<LoanTypeResponse>> GetAllLoanTypes(Pageable pageable);

    Task<LoanTypeResponse?> GetOneLoanType(Guid loanTypeId);

    Task<LoanTypeResponse?> CreateLoanType(LoanTypeCreateRequest createRequest);

    Task<LoanTypeResponse?> UpdateLoanType(Guid loanTypeId, LoanTypeUpdateRequest updateRequest);

    #endregion

    #region TransactionCodeController methods

    Task<Page<TransactionCodeResponse>> GetAllTransactionCodes(TransactionCodeFilterQuery filter, Pageable pageable);

    Task<TransactionCodeResponse?> GetOneTransactionCode(Guid transactionCodeId);

    #endregion

    #region TransactionController methods

    Task<Page<TransactionResponse>> GetAllTransactions(TransactionFilterQuery filter, Pageable pageable);

    Task<Page<TransactionResponse>> GetAllTransactionsForAccount(Guid accountId, TransactionFilterQuery filter, Pageable pageable);

    Task<TransactionResponse?> GetOneTransaction(Guid transactionId);

    Task<TransactionCreateResponse?> CreateTransaction(TransactionCreateRequest createRequest); //TODO: make transaction response

    Task<TransactionResponse?> UpdateTransaction(Guid transactionId, TransactionUpdateRequest updateRequest);

    #endregion

    #region TransactionTemplateController methods

    Task<Page<TransactionTemplateResponse>> GetAllTransactionTemplates(Pageable pageable);

    Task<TransactionTemplateResponse?> GetOneTransactionTemplate(Guid transactionTemplateId);

    Task<TransactionTemplateResponse?> CreateTransactionTemplate(TransactionTemplateCreateRequest createRequest);

    Task<TransactionTemplateResponse?> UpdateTransactionTemplate(Guid transactionTemplateId, TransactionTemplateUpdateRequest updateRequest);

    #endregion

    #region UserController methods

    Task<Page<UserResponse>> GetAllUsers(UserFilterQuery filter, Pageable pageable);

    Task<UserResponse?> GetOneUser(Guid userId);

    Task<UserLoginResponse?> Login(UserLoginRequest loginRequest);

    Task Activate(UserActivationRequest activationRequest, string token);

    Task RequestPasswordReset(UserRequestPasswordResetRequest passwordResetRequest);

    Task PasswordReset(UserPasswordResetRequest passwordResetRequest, string token);

    Task UpdateUserPermission(Guid userId, UserUpdatePermissionRequest updatePermissionRequest);

    #endregion
}

internal partial class UserServiceHttpClient(IHttpClientFactory httpClientFactory) : IUserServiceHttpClient
{
    private readonly IHttpClientFactory m_HttpClientFactory = httpClientFactory;
}

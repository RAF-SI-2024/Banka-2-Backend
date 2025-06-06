using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.Http.Clients.User;

namespace Bank.Link.Service;

internal interface IDataService
{
    List<CurrencyResponse> GetCurrencies();

    List<TransactionCodeResponse> GetTransactionCodes();

    List<AccountTypeResponse> GetAccountTypes();

    CurrencyResponse? GetCurrencyByCode(string code);

    TransactionCodeResponse? GetTransactionCodeById(Guid id);

    TransactionCodeResponse? GetTransactionCodeByCode(string code);

    AccountTypeResponse? GetAccountType(string accountNumber);
}

internal class DataService : IDataService
{
    private readonly List<CurrencyResponse>        m_Currencies;
    private readonly List<TransactionCodeResponse> m_TransactionCodes;
    private readonly List<AccountTypeResponse>     m_AccountTypes;
    private readonly IUserServiceHttpClient        m_UserServiceHttpClient;

    public DataService(IEnumerable<CurrencyResponse> currencies, IEnumerable<TransactionCodeResponse> transactionCodes, IEnumerable<AccountTypeResponse> accountTypes,
                       IUserServiceHttpClient        userServiceHttpClient)
    {
        m_UserServiceHttpClient = userServiceHttpClient;
        m_Currencies            = currencies.ToList();
        m_TransactionCodes      = transactionCodes.ToList();
        m_AccountTypes          = accountTypes.ToList();

        Instantiate()
        .Wait();
    }

    private async Task Instantiate()
    {
        if (m_Currencies.Count == 0)
        {
            var currencies = await m_UserServiceHttpClient.GetAllCurrencies(new());

            m_Currencies.AddRange(currencies);
        }

        if (m_TransactionCodes.Count == 0)
        {
            var transactionCodes = await m_UserServiceHttpClient.GetAllTransactionCodes(new(), Pageable.Create(1, 100));

            m_TransactionCodes.AddRange(transactionCodes.Items);
        }
    }

    public List<CurrencyResponse> GetCurrencies() => m_Currencies;

    public List<TransactionCodeResponse> GetTransactionCodes() => m_TransactionCodes;

    public List<AccountTypeResponse> GetAccountTypes() => m_AccountTypes;

    public CurrencyResponse? GetCurrencyByCode(string code) => m_Currencies.FirstOrDefault(currency => currency.Code == code);

    public TransactionCodeResponse? GetTransactionCodeById(Guid id) => m_TransactionCodes.FirstOrDefault(transactionCode => transactionCode.Id == id);

    public TransactionCodeResponse? GetTransactionCodeByCode(string code) => m_TransactionCodes.FirstOrDefault(transactionCode => transactionCode.Code == code);

    public AccountTypeResponse? GetAccountType(string accountNumber) =>
    m_AccountTypes.FirstOrDefault(accountType => accountType.Code == accountNumber[^2..], m_AccountTypes.First());
}

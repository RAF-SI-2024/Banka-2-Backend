using Bank.Application.Domain;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Constant
    {
        public const string AccountName                = "Account Name";
        public const string BankName                   = "Bank Name";
        public const string BankCode                   = "222";
        public const string CompanyName                = "Company Name";
        public const string CompanyActivityCode        = "1111";
        public const string CurrencyName               = "Currency Name";
        public const string CurrencyCode               = "Currency Code";
        public const string TransactionCodeName        = "Transaction Code Name";
        public const string TransactionsCode           = "Transaction Code";
        public const string CurrencySymbol             = "Currency Symbol";
        public const string BankBaseUrl                = "https://banka-2.si.raf.edu.rs";
        public const string AccountTypeName            = "Account Type Name";
        public const string CardTypeName               = "Card Type Name";
        public const string CardName                   = "Card Name";
        public const string CardCVV                    = "Card CVV";
        public const string LoanTypeName               = "Loan Type Name";
        public const string CountryName                = "Country Name";
        public const string AccountTypeCode            = "Account Type Name";
        public const string Email                      = "Email";
        public const string Token                      = "xxxxxxxxxx.yyyyyyyyyy.zzzzzzzzzz";
        public const string Username                   = "Username";
        public const string Password                   = "Password";
        public const string FirstName                  = "First Name";
        public const string LastName                   = "Last Name";
        public const string Address                    = "Address";
        public const string Phone                      = "+38160000000000";
        public const string Department                 = "Department";
        public const string AccountNumber              = "2220000XXXXXXXXXXX";
        public const string Office                     = "XXXX";
        public const string ConfirmationCode           = "XXXXXX";
        public const string UniqueIdentificationNumber = "XXXXXXXXXXXXX";
        public const string Description                = "Description";
        public const string TemplateName               = "Template Name";
        public const string Ticker                     = "ABCD";
        public const string Acronym                    = "Acronym";
        public const string MIC                        = "MIC";
        public const string ForexPairName              = "Forex Pair Name";
        public const string SecurityName              = "Security Name";
        public const string FutureContractName         = "Future Contract Name";
        public const string StockName                  = "Stock Name";
        public const string OptionName                 = "Option Name";

        public const decimal DailyLimit                   = 2000.00m;
        public const decimal Commission                   = 0.5m;
        public const decimal Rate                         = 100.00m;
        public const decimal Amount                       = 200.00m;
        public const decimal InverseRate                  = 1 / Rate;
        public const decimal MonthlyLimit                 = 10000.00m;
        public const decimal Balance                      = 5000.00m;
        public const decimal LoanTypeMargin               = 3.5m;
        public const decimal Price                        = 100.00m;
        public const decimal OpeningPrice                 = 100.00m;
        public const decimal ClosePrice                   = 100.00m;
        public const decimal HighPrice                    = 100.00m;
        public const decimal LowPrice                     = 100.00m;
        public const decimal AskPrice                     = 100.00m;
        public const int     AskSize                      = 1000;
        public const decimal BidPrice                     = 100.00m;
        public const int     BidSize                      = 1000;
        public const decimal StrikePrice                  = 100.00m;
        public const decimal MaintenanceDecimal           = 0.01m;
        public const decimal PricePerUnit                 = 100.00m;
        public const decimal PriceChangeInInterval        = 0.01m;
        public const decimal PriceChangePercentInInterval = 0.01m;
        public const decimal ImpliedVolatility            = 0.01m;

        public const int Period            = 64;
        public const int Volume            = 1000;
        public const int ContractSize      = 1000;
        public const int Quantity          = 1000;
        public const int ContractCount     = 1;
        public const int RemainingPortions = 1000;

        public const long Permissions = 0;

        public const bool Boolean = true;

        public static readonly Gender            Gender            = Gender.Male;
        public static readonly Role              Role              = Role.Client;
        public static readonly InstallmentStatus InstallmentStatus = InstallmentStatus.Paid;
        public static readonly InterestType      InterestType      = InterestType.Variable;
        public static readonly LoanStatus        LoanStatus        = LoanStatus.Active;
        public static readonly TransactionStatus TransactionStatus = TransactionStatus.Completed;
        public static readonly Liquidity         Liquidity         = Liquidity.Medium;
        public static readonly ContractUnit      ContractUnit      = ContractUnit.Barrel;
        public static readonly OptionType        OptionType        = OptionType.Call;
        public static readonly OrderType         OrderType         = OrderType.Limit;
        public static readonly OrderStatus       OrderStatus       = OrderStatus.NeedsApproval;
        public static readonly Direction         Direction         = Direction.Buy;
        public static readonly PermissionType    PermissionType    = PermissionType.Set;

        public static readonly Guid Id = Guid.Parse("12345678-1234-1234-1234-123456789abc");

        public static readonly DateOnly CreationDate = DateOnly.FromDateTime(DateTime.UtcNow);
        public static readonly DateTime CreatedAt    = DateTime.UtcNow;
        public static readonly DateTime ModifiedAt   = DateTime.UtcNow;
        public static readonly TimeSpan TimeZone     = TimeSpan.Zero;
    }
}

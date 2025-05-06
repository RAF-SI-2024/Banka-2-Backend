namespace Bank.Application.Endpoints;

public static partial class Endpoints
{
    public const string ApiBase = "api/v1";
}

#region Exchange Service

public static partial class Endpoints
{
    public static class Stock
    {
        public const string Base      = $"{ApiBase}/stock";
        public const string BaseDaily = $"{Base}/daily";

        public const string GetAll      = $"{Base}";
        public const string GetOne      = $"{Base}/{{id:guid}}";
        public const string GetOneDaily = $"{BaseDaily}/{{id:guid}}";
    }

    public static class ForexPair
    {
        public const string Base      = $"{ApiBase}/forex/pair";
        public const string BaseDaily = $"{Base}/daily";

        public const string GetAll      = $"{Base}";
        public const string GetOne      = $"{Base}/{{id:guid}}";
        public const string GetOneDaily = $"{BaseDaily}/{{id:guid}}";
    }

    public static class FutureContract
    {
        public const string Base      = $"{ApiBase}/future/contract";
        public const string BaseDaily = $"{Base}/daily";

        public const string GetAll      = $"{Base}";
        public const string GetOne      = $"{Base}/{{id:guid}}";
        public const string GetOneDaily = $"{BaseDaily}/{{id:guid}}";
    }

    public static class Option
    {
        public const string Base      = $"{ApiBase}/option";
        public const string BaseDaily = $"{Base}/daily";

        public const string GetAll      = $"{Base}";
        public const string GetOne      = $"{Base}/{{id:guid}}";
        public const string GetOneDaily = $"{BaseDaily}/{{id:guid}}";
    }
}

#endregion

#region User Service

public static partial class Endpoints
{
    public static class User
    {
        public const string Base = $"{ApiBase}/users";

        public const string GetAll               = $"{Base}";
        public const string GetOne               = $"{Base}/{{id:guid}}";
        public const string Login                = $"{Base}/login";
        public const string Activate             = $"{Base}/activate";
        public const string RequestPasswordReset = $"{Base}/password-reset/request";
        public const string PasswordReset        = $"{Base}/password-reset";
        public const string UpdatePermission     = $"{Base}/{{id:guid}}/permission";
    }
}

public static partial class Endpoints
{
    public static class Employee
    {
        public const string Base = $"{ApiBase}/employees";

        public const string GetAll = $"{Base}";
        public const string Create = $"{Base}";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class Client
    {
        public const string Base = $"{ApiBase}/clients";

        public const string GetAll = $"{Base}";
        public const string Create = $"{Base}";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class Company
    {
        public const string Base = $"{ApiBase}/companies";

        public const string GetAll = $"{Base}";
        public const string Create = $"{Base}";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class Currency
    {
        public const string Base = $"{ApiBase}/currencies";

        public const string GetOne       = $"{Base}/{{id:guid}}";
        public const string GetAll       = $"{Base}";
        public const string GetAllSimple = $"{Base}/simple";
        public const string GetOneSimple = $"{Base}/simple/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class Country
    {
        public const string Base = $"{ApiBase}/countries";

        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
    }
}

public static partial class Endpoints
{
    public static class AccountType
    {
        public const string Base = $"{ApiBase}/accounts/types";

        public const string GetAll = $"{Base}";
        public const string GetOne = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class Account
    {
        public const string Base = $"{ApiBase}/accounts";

        public const string GetAll          = $"{Base}";
        public const string GetAllForClient = $"{Client.Base}/{{clientId:guid}}/accounts";
        public const string GetOne          = $"{Base}/{{id:guid}}";
        public const string Create          = $"{Base}";
        public const string UpdateEmployee  = $"{Base}/employee/{{id:guid}}";
        public const string UpdateClient    = $"{Base}/client/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class AccountCurrency
    {
        public const string Base = $"{ApiBase}/accounts/currencies";

        public const string GetAll       = $"{Base}";
        public const string GetOne       = $"{Base}/{{id:guid}}";
        public const string Create       = $"{Base}";
        public const string UpdateClient = $"{Base}/{{id:guid}}/client";
    }
}

public static partial class Endpoints
{
    public static class Card
    {
        public const string Base = $"{ApiBase}/cards";

        public const string GetAll           = $"{Base}";
        public const string GetAllForAccount = $"{Account.Base}/{{accountId:guid}}/cards";
        public const string GetAllForClient  = $"{Client.Base}/{{clientId:guid}}/cards";
        public const string GetOne           = $"{Base}/{{id:guid}}";
        public const string Create           = $"{Base}";
        public const string UpdateStatus     = $"{Base}/{{id:guid}}/status";
        public const string UpdateLimit      = $"{Base}/{{id:guid}}/limit";
    }
}

public static partial class Endpoints
{
    public static class CardType
    {
        public const string Base = $"{ApiBase}/cards/types";

        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
    }
}

public static partial class Endpoints
{
    public static class Exchange
    {
        public const string Base = $"{ApiBase}/exchanges";

        public const string GetOne          = $"{Base}/{{id:guid}}";
        public const string GetAll          = $"{Base}";
        public const string GetByCurrencies = $"{Base}/currencies";
        public const string MakeExchange    = $"{Base}";
        public const string Update          = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class TransactionCode
    {
        public const string Base = $"{ApiBase}/transactions/codes";

        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
    }
}

public static partial class Endpoints
{
    public static class TransactionTemplate
    {
        public const string Base = $"{ApiBase}/transactions/templates";

        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class Transaction
    {
        public const string Base = $"{ApiBase}/transactions";

        public const string GetAll           = $"{Base}";
        public const string GetAllForAccount = $"{Account.Base}/{{accountId:guid}}/transactions";
        public const string GetOne           = $"{Base}/{{id:guid}}";
        public const string Create           = $"{Base}";
        public const string ProcessInternal  = $"{Base}/internal";
        public const string ProcessExternal  = $"{Base}/external";
        public const string Update           = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class LoanType
    {
        public const string Base = $"{ApiBase}/loans/types";

        public const string GetAll = $"{Base}";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class Loan
    {
        public const string Base = $"{ApiBase}/loans";

        public const string GetAll          = $"{Base}";
        public const string GetOne          = $"{Base}/{{id:guid}}";
        public const string Create          = $"{Base}";
        public const string Update          = $"{Base}/{{id:guid}}";
        public const string GetAllForClient = $"{Client.Base}/{{clientId:guid}}/loans";
    }
}

public static partial class Endpoints
{
    public static class Installment
    {
        public const string Base = $"{ApiBase}/installments";

        public const string GetAllByLoan = $"{Loan.Base}/{{loanId:guid}}/installments";
        public const string GetOne       = $"{Base}/{{id:guid}}";
        public const string Create       = $"{Base}";
        public const string Update       = $"{Base}/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class StockExchange
    {
        public const string Base   = $"{ApiBase}/exchanges";
        public const string GetAll = $"{Base}";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string Create = $"{Base}";
    }
}

public static partial class Endpoints
{
    public static class Order
    {
        public const string Base = $"{ApiBase}/orders";

        public const string GetAll = $"{Base}";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}

#endregion

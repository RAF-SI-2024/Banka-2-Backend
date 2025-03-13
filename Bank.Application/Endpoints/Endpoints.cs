namespace Bank.Application.Endpoints;

public static partial class Endpoints
{
    public const string ApiBase = "api/v1";
}

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

        public const string GetAll         = $"{Base}";
        public const string GetAllAccounts = $"{Base}/{{id:guid}}/accounts";
        public const string Create         = $"{Base}";
        public const string GetOne         = $"{Base}/{{id:guid}}";
        public const string Update         = $"{Base}/{{id:guid}}";
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
        public const string Base   = $"{ApiBase}/currencies";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
    }
}

public static partial class Endpoints
{
    public static class Country
    {
        public const string Base   = $"{ApiBase}/countries";
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
        public const string Base           = $"{ApiBase}/accounts";
        public const string GetAll         = $"{Base}";
        public const string GetAllCards    = $"{Base}/{{id:guid}}/cards";
        public const string GetOne         = $"{Base}/{{id:guid}}";
        public const string Create         = $"{Base}";
        public const string UpdateEmployee = $"{Base}/employee/{{id:guid}}";
        public const string UpdateClient   = $"{Base}/client/{{id:guid}}";
    }
}

public static partial class Endpoints
{
    public static class AccountCurrency
    {
        public const string Base         = $"{ApiBase}/accounts/currencies";
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
        public const string Base           = $"{ApiBase}/cards";
        public const string GetOne         = $"{Base}/{{id:guid}}";
        public const string GetAll         = $"{Base}";
        public const string Create         = $"{Base}";
        public const string UpdateEmployee = $"{Base}/{{id:guid}}/employee";
        public const string UpdateClient   = $"{Base}/{{id:guid}}/client";
    }
}

public static partial class Endpoints
{
    public static class CardType
    {
        public const string Base   = $"{ApiBase}/cards/types";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
    }
}

public static partial class Endpoints
{
    public static class Exchange
    {
        public const string Base            = $"{ApiBase}/exchange";
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
        public const string Base   = $"{ApiBase}/transaction/codes";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
    }
}

public static partial class Endpoints
{
    public static class TransactionTemplate
    {
        public const string Base   = $"{ApiBase}/transaction/templates";
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
        public const string Base   = $"{ApiBase}/transaction";
        public const string GetOne = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
    }
}

#endregion

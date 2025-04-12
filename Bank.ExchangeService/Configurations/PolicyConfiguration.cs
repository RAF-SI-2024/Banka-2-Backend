using DomainRole = Bank.Application.Domain.Role;

namespace Bank.ExchangeService.Configurations;

public static partial class Configuration
{
    public static class Policy
    {
        public const string FrontendApplication = nameof(FrontendApplication);

        public static class Role
        {
            public const string Admin    = nameof(DomainRole.Admin);
            public const string Employee = nameof(DomainRole.Employee);
            public const string Client   = nameof(DomainRole.Client);
        }
    }
}

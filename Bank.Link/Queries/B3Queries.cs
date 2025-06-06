namespace Bank.Link.Queries;

internal static partial class Query
{
    internal static class B3
    {
        internal class AccountFilter(string? accountNumber = null, string? firstName = null, string? lastName = null)
        {
            public string? AccountNumber { set; get; } = accountNumber;
            public string? FirstName     { set; get; } = firstName;
            public string? LastName      { set; get; } = lastName;
        }
    }
}

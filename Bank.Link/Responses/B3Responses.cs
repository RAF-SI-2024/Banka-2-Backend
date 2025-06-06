namespace Bank.Link.Responses;

internal static partial class Response
{
    internal static class B3
    {
        internal class ClientResponse
        {
            public required long     Id        { set; get; }
            public required string   FirstName { set; get; }
            public required string   LastName  { set; get; }
            public required string   Email     { set; get; }
            public required string   Address   { set; get; }
            public required string   Phone     { set; get; }
            public required string   Gender    { set; get; }
            public required DateOnly BirthDate { set; get; }
            public required string   Jmbg      { set; get; }
            public required string   Username  { set; get; }
        }

        internal class AccountClientResponse
        {
            public required long   Id        { set; get; }
            public required string FirstName { set; get; }
            public required string LastName  { set; get; }
            public required string Email     { set; get; }
        }

        internal class AccountResponse
        {
            public required string                Name                { set; get; }
            public required string                AccountNumber       { set; get; }
            public required long                  ClientId            { set; get; }
            public required long?                 CompanyId           { set; get; }
            public required long                  CreatedByEmployeeId { set; get; }
            public required string                CreationDate        { set; get; }
            public required string                ExpirationDate      { set; get; }
            public required string                CurrencyCode        { set; get; }
            public required string                Status              { set; get; }
            public required decimal               Balance             { set; get; }
            public required decimal               AvailableBalance    { set; get; }
            public required decimal               DailyLimit          { set; get; }
            public required decimal               MonthlyLimit        { set; get; }
            public required decimal               DailySpending       { set; get; }
            public required decimal               MonthlySpending     { set; get; }
            public required string                OwnershipType       { set; get; }
            public required AccountClientResponse Owner               { set; get; }
            public required string                AccountCategory     { set; get; }
        }

        internal class TransactionResponse
        {
            public required long    Id                { set; get; }
            public required decimal Amount            { set; get; }
            public required string  PaymentPurpose    { set; get; }
            public required string  FromAccountNumber { set; get; }
            public required string  ToAccountNumber   { set; get; }
            public required string  RecieverName      { set; get; }
            public required string  PaymentCode       { set; get; }
            public required string  CurrencyCode      { set; get; }
        }

        internal class Page<T>
        {
            public List<T> Content       { get; set; } = [];
            public int     Number        { get; set; }
            public int     Size          { get; set; }
            public int     TotalElements { get; set; }
        }
    }
}

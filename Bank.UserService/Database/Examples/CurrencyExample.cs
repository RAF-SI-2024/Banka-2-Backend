using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

file static class Values
{
    public static readonly Guid   Id          = Guid.Parse("5efa312a-5ab6-4950-9579-0f605aeab4f8");
    public const           string Name        = "Dolar";
    public const           string Code        = "USD";
    public const           string Symbol      = "$";
    public const           string Description = "Zvanična valuta Sjedinjenih Američkih Država";
    public const           bool   Status      = true;
}

public static partial class Example
{
    public static class Currency
    {
        public static readonly CurrencyResponse Response = new()
                                                           {
                                                               Id          = Values.Id,
                                                               Name        = Values.Name,
                                                               Code        = Values.Code,
                                                               Symbol      = Values.Symbol,
                                                               Countries   = [],
                                                               Description = Values.Description,
                                                               Status      = Values.Status,
                                                               CreatedAt   = DateTime.Now,
                                                               ModifiedAt  = DateTime.Now
                                                           };

        public static readonly CurrencySimpleResponse SimpleResponse = new()
                                                                       {
                                                                           Id          = Values.Id,
                                                                           Name        = Values.Name,
                                                                           Code        = Values.Code,
                                                                           Symbol      = Values.Symbol,
                                                                           Description = Values.Description,
                                                                           Status      = Values.Status,
                                                                           CreatedAt   = DateTime.Now,
                                                                           ModifiedAt  = DateTime.Now
                                                                       };
    }
}

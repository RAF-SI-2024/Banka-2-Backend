using Bank.Application.Responses;

namespace Bank.ExchangeService.Database.Examples;

file static class Values
{
    public static readonly Guid   Id            = Guid.Parse("3f4b1e6e-a2f5-4e3b-8f88-2f70a6b42b19");
    public const           string AccountNumber = "222001112345678922";
}

public static partial class Example
{
    public static class Account
    {
        public static readonly AccountResponse Response = new()
                                                          {
                                                              Id                = Values.Id,
                                                              AccountNumber     = Values.AccountNumber,
                                                              Office            = "0000",
                                                              Name              = "Savings Account",
                                                              Balance           = 5000.00m,
                                                              AvailableBalance  = 4500.50m,
                                                              Type              = null!,
                                                              Currency          = null!,
                                                              Employee          = null!,
                                                              Client            = null!,
                                                              AccountCurrencies = [],
                                                              DailyLimit        = 2000.00m,
                                                              MonthlyLimit      = 50000.00m,
                                                              CreationDate      = new(2023, 5, 15),
                                                              ExpirationDate    = new(2033, 5, 15),
                                                              Status            = true,
                                                              CreatedAt         = DateTime.UtcNow,
                                                              ModifiedAt        = DateTime.UtcNow
                                                          };

        public static readonly AccountSimpleResponse SimpleResponse = new()
                                                                      {
                                                                          Id            = Values.Id,
                                                                          AccountNumber = Values.AccountNumber
                                                                      };
    }
}

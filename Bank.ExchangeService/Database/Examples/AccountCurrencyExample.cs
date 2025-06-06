using Bank.Application.Responses;

namespace Bank.ExchangeService.Database.Examples;

public static partial class Example
{
    public static class AccountCurrency
    {
        public static readonly AccountCurrencyResponse Response = new()
                                                                  {
                                                                      Id               = Guid.Parse("d4e5f6a7-b8c9-40d1-a2b3-c4d5e6f78901"),
                                                                      Account          = null!,
                                                                      Employee         = null!,
                                                                      Currency         = null!,
                                                                      Balance          = 12000.75m,
                                                                      AvailableBalance = 8000.50m,
                                                                      DailyLimit       = 1000,
                                                                      MonthlyLimit     = 3000,
                                                                      CreatedAt        = DateTime.UtcNow,
                                                                      ModifiedAt       = DateTime.UtcNow
                                                                  };
    }
}

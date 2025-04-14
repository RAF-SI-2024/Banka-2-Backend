using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Exchange
    {
        public static readonly ExchangeMakeExchangeRequest DefaultMakeExchangeRequest = new()
                                                                                        {
                                                                                            CurrencyFromId = Constant.Id,
                                                                                            CurrencyToId   = Constant.Id,
                                                                                            Amount         = Constant.Amount,
                                                                                            AccountId      = Constant.Id
                                                                                        };

        public static readonly ExchangeUpdateRequest DefaultUpdateRequest = new()
                                                                            {
                                                                                Commission = Constant.Commission,
                                                                            };

        public static readonly ExchangeResponse DefaultResponse = new()
                                                                  {
                                                                      Id           = Constant.Id,
                                                                      CurrencyFrom = Currency.DefaultSimpleResponse,
                                                                      CurrencyTo   = Currency.DefaultSimpleResponse,
                                                                      Commission   = Constant.Commission,
                                                                      Rate         = Constant.Rate,
                                                                      InverseRate  = Constant.InverseRate,
                                                                      CreatedAt    = Constant.CreatedAt,
                                                                      ModifiedAt   = Constant.ModifiedAt,
                                                                  };
    }
}

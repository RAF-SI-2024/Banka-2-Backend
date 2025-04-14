using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class CardType
    {
        public static readonly CardTypeResponse DefaultResponse = new()
                                                                  {
                                                                      Id         = Constant.Id,
                                                                      Name       = Constant.CardTypeName,
                                                                      CreatedAt  = Constant.CreatedAt,
                                                                      ModifiedAt = Constant.ModifiedAt,
                                                                  };
    }
}

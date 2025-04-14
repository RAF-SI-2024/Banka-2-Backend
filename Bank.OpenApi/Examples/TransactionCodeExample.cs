using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public class TransactionCode
    {
        public static readonly TransactionCodeResponse DefaultResponse = new()
                                                                         {
                                                                             Id         = Constant.Id,
                                                                             Code       = Constant.TransactionsCode,
                                                                             Name       = Constant.TransactionCodeName,
                                                                             CreatedAt  = Constant.CreatedAt,
                                                                             ModifiedAt = Constant.ModifiedAt,
                                                                         };
    }
}

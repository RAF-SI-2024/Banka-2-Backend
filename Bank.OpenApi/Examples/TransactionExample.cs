using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Transaction
    {
        public static readonly TransactionCreateRequest DefaultCreateRequest = new()
                                                                               {
                                                                                   FromAccountNumber = Constant.AccountNumber,
                                                                                   FromCurrencyId    = Constant.Id,
                                                                                   ToAccountNumber   = Constant.AccountNumber,
                                                                                   ToCurrencyId      = Constant.Id,
                                                                                   Amount            = Constant.Amount,
                                                                                   CodeId            = Constant.Id,
                                                                                   ReferenceNumber   = Constant.UniqueIdentificationNumber,
                                                                                   Purpose           = Constant.Description,
                                                                                   ConfirmationCode  = Constant.ConfirmationCode,
                                                                               };

        public static readonly TransactionUpdateRequest DefaultUpdateRequest = new()
                                                                               {
                                                                                   Status = Constant.TransactionStatus,
                                                                               };

        public static readonly TransactionResponse DefaultResponse = new()
                                                                     {
                                                                         Id              = Constant.Id,
                                                                         FromAccount     = Account.DefaultSimpleResponse,
                                                                         FromCurrency    = Currency.DefaultResponse,
                                                                         FromAmount      = Constant.Amount,
                                                                         ToAccount       = Account.DefaultSimpleResponse,
                                                                         ToCurrency      = Currency.DefaultResponse,
                                                                         ToAmount        = Constant.Amount,
                                                                         Code            = TransactionCode.DefaultResponse,
                                                                         ReferenceNumber = Constant.UniqueIdentificationNumber,
                                                                         Purpose         = Constant.Description,
                                                                         Status          = Constant.TransactionStatus,
                                                                         CreatedAt       = Constant.CreatedAt,
                                                                         ModifiedAt      = Constant.ModifiedAt,
                                                                     };

        public static readonly TransactionCreateResponse DefaultCreateResponse = new()
                                                                                 {
                                                                                     Id              = Constant.Id,
                                                                                     FromAmount      = Constant.Amount,
                                                                                     Code            = TransactionCode.DefaultResponse,
                                                                                     ReferenceNumber = Constant.UniqueIdentificationNumber,
                                                                                     Purpose         = Constant.Description,
                                                                                     Status          = Constant.TransactionStatus,
                                                                                     CreatedAt       = Constant.CreatedAt,
                                                                                     ModifiedAt      = Constant.ModifiedAt,
                                                                                 };
    }
}

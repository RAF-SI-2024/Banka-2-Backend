using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class TransactionTemplate
    {
        public static readonly TransactionTemplateCreateRequest DefaultCreateRequest = new()
                                                                                       {
                                                                                           AccountNumber = Constant.AccountNumber,
                                                                                           Name          = Constant.TemplateName,
                                                                                       };

        public static readonly TransactionTemplateUpdateRequest DefaultUpdateRequest = new()
                                                                                       {
                                                                                           AccountNumber = Constant.AccountNumber,
                                                                                           Deleted       = Constant.Boolean,
                                                                                           Name          = Constant.TemplateName,
                                                                                       };

        public static readonly TransactionTemplateResponse DefaultResponse = new()
                                                                             {
                                                                                 Id            = Constant.Id,
                                                                                 Client        = Client.DefaultSimpleResponse,
                                                                                 Name          = Constant.TemplateName,
                                                                                 AccountNumber = Constant.AccountNumber,
                                                                                 Deleted       = Constant.Boolean,
                                                                                 CreatedAt     = Constant.CreatedAt,
                                                                                 ModifiedAt    = Constant.ModifiedAt,
                                                                             };

        public static readonly TransactionTemplateSimpleResponse DefaultSimpleResponse = new()
                                                                                         {
                                                                                             Id            = Constant.Id,
                                                                                             Name          = Constant.TemplateName,
                                                                                             AccountNumber = Constant.AccountNumber,
                                                                                             Deleted       = Constant.Boolean,
                                                                                         };
    }
}

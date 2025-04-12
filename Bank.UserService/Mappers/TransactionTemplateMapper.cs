using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class TransactionTemplateMapper
{
    public static TransactionTemplateResponse ToResponse(this TransactionTemplate transactionTemplate)
    {
        return new TransactionTemplateResponse
               {
                   Id            = transactionTemplate.Id,
                   Name          = transactionTemplate.Name,
                   AccountNumber = transactionTemplate.AccountNumber,
                   Client = transactionTemplate.Client?.ToClient()
                                               .ToSimpleResponse()!,
                   Deleted    = transactionTemplate.Deleted,
                   CreatedAt  = transactionTemplate.CreatedAt,
                   ModifiedAt = transactionTemplate.ModifiedAt
               };
    }

    public static TransactionTemplateSimpleResponse ToSimpleResponse(this TransactionTemplate transactionTemplate)
    {
        return new TransactionTemplateSimpleResponse
               {
                   Id            = transactionTemplate.Id,
                   Name          = transactionTemplate.Name,
                   AccountNumber = transactionTemplate.AccountNumber,
                   Deleted       = transactionTemplate.Deleted
               };
    }

    public static TransactionTemplate ToTransactionTemplate(this TransactionTemplateCreateRequest transactionTemplateCreateRequest, Guid clientId)
    {
        return new TransactionTemplate
               {
                   Id            = Guid.NewGuid(),
                   ClientId      = clientId,
                   Name          = transactionTemplateCreateRequest.Name,
                   AccountNumber = transactionTemplateCreateRequest.AccountNumber,
                   Deleted       = false,
                   CreatedAt     = DateTime.UtcNow,
                   ModifiedAt    = DateTime.UtcNow
               };
    }

    public static TransactionTemplate ToTransactionTemplate(this TransactionTemplate transactionTemplate, TransactionTemplateUpdateRequest transactionTemplateUpdateRequest)
    {
        transactionTemplate.AccountNumber = transactionTemplateUpdateRequest.AccountNumber;
        transactionTemplate.Name          = transactionTemplateUpdateRequest.Name;
        transactionTemplate.Deleted       = transactionTemplateUpdateRequest.Deleted;
        transactionTemplate.ModifiedAt    = DateTime.UtcNow;

        return transactionTemplate;
    }
}

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ITransactionTemplateService
{
    Task<Result<Page<TransactionTemplateResponse>>> GetAll(Pageable pageable);

    Task<Result<TransactionTemplateResponse>> GetOne(Guid id);

    Task<Result<TransactionTemplateResponse>> Create(TransactionTemplateCreateRequest transactionTemplateCreateRequest);

    Task<Result<TransactionTemplateResponse>> Update(TransactionTemplateUpdateRequest transactionTemplateUpdateRequest, Guid id);
}

public class TransactionTemplateService(ITransactionTemplateRepository transactionTemplateRepository, IAuthorizationService authorizationService) : ITransactionTemplateService
{
    private readonly ITransactionTemplateRepository m_TransactionTemplateRepository = transactionTemplateRepository;
    private readonly IAuthorizationService          m_AuthorizationService          = authorizationService;

    public async Task<Result<Page<TransactionTemplateResponse>>> GetAll(Pageable pageable)
    {
        var page = await m_TransactionTemplateRepository.FindAll(pageable);

        var transactionTemplateResponses = page.Items.Select(transactionTemplate => transactionTemplate.ToResponse())
                                               .ToList();

        return Result.Ok(new Page<TransactionTemplateResponse>(transactionTemplateResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<TransactionTemplateResponse>> GetOne(Guid id)
    {
        var transactionTemplate = await m_TransactionTemplateRepository.FindById(id);

        if (transactionTemplate == null)
            return Result.NotFound<TransactionTemplateResponse>($"No Transaction Template found with Id: {id}");

        if (transactionTemplate.ClientId != m_AuthorizationService.UserId)
            return Result.Unauthorized<TransactionTemplateResponse>();

        return Result.Ok(transactionTemplate.ToResponse());
    }

    public async Task<Result<TransactionTemplateResponse>> Create(TransactionTemplateCreateRequest transactionTemplateCreateRequest)
    {
        var transactionTemplate = await m_TransactionTemplateRepository.Add(transactionTemplateCreateRequest.ToTransactionTemplate(m_AuthorizationService.UserId));

        return Result.Ok(transactionTemplate.ToResponse());
    }

    public async Task<Result<TransactionTemplateResponse>> Update(TransactionTemplateUpdateRequest request, Guid id)
    {
        var dbTransactionTemplate = await m_TransactionTemplateRepository.FindById(id);
    
        if (dbTransactionTemplate is null)
            return Result.NotFound<TransactionTemplateResponse>($"No Transaction Template found with Id: {id}");
    
        var order = await m_TransactionTemplateRepository.Update(dbTransactionTemplate.ToTransactionTemplate(request));
    
        return Result.Ok(order.ToResponse());
    }
}

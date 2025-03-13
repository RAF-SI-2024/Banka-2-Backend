using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ITransactionService
{
    Task<Result<Page<TransactionResponse>>> GetAll(TransactionFilterQuery transactionFilterQuery, Pageable pageable);

    Task<Result<TransactionResponse>> GetOne(Guid id);

    Task<Result<TransactionCreateResponse>> Create(TransactionCreateRequest transactionCreateRequest);

    Task<Result<TransactionResponse>> Update(TransactionUpdateRequest transactionUpdateRequest, Guid id);
}

public class TransactionService(ITransactionRepository transactionRepository, ITransactionCodeRepository transactionCodeRepository, IAuthorizationService authorizationService)
: ITransactionService
{
    private readonly ITransactionRepository     m_TransactionRepository     = transactionRepository;
    private readonly ITransactionCodeRepository m_TransactionCodeRepository = transactionCodeRepository;
    private readonly IAuthorizationService      m_AuthorizationService      = authorizationService;

    public async Task<Result<Page<TransactionResponse>>> GetAll(TransactionFilterQuery transactionFilterQuery, Pageable pageable)
    {
        var page = await m_TransactionRepository.FindAll(transactionFilterQuery, pageable);

        var transactionResponses = page.Items.Select(transaction => transaction.ToResponse())
                                       .ToList();

        return Result.Ok(new Page<TransactionResponse>(transactionResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<TransactionResponse>> GetOne(Guid id)
    {
        var transaction = await m_TransactionRepository.FindById(id);

        if (transaction is null)
            return Result.NotFound<TransactionResponse>($"No Transaction found with Id: {id}");

        if (m_AuthorizationService.Role     == Role.Client && transaction.FromAccount?.ClientId != m_AuthorizationService.UserId &&
            transaction.ToAccount?.ClientId != m_AuthorizationService.UserId)
            return Result.Unauthorized<TransactionResponse>();

        return Result.Ok(transaction.ToResponse());
    }

    public async Task<Result<TransactionCreateResponse>> Create(TransactionCreateRequest transactionCreateRequest)
    {
        var code = await m_TransactionCodeRepository.FindById(transactionCreateRequest.CodeId);

        if (code == null)
            return Result.BadRequest<TransactionCreateResponse>("Invalid data.");

        var transaction = await m_TransactionRepository.Add(transactionCreateRequest.ToTransaction(code));

        return Result.Ok(transaction.ToCreateResponse());
    }

    public async Task<Result<TransactionResponse>> Update(TransactionUpdateRequest transactionUpdateRequest, Guid id)
    {
        var oldTransaction = await m_TransactionRepository.FindById(id);

        if (oldTransaction is null)
            return Result.NotFound<TransactionResponse>($"No Transaction found with Id: {id}");

        var account = await m_TransactionRepository.Update(oldTransaction, transactionUpdateRequest.ToTransaction(oldTransaction));

        return Result.Ok(account.ToResponse());
    }
}

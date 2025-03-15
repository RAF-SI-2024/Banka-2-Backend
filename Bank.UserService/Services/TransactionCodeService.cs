using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ITransactionCodeService
{
    Task<Result<Page<TransactionCodeResponse>>> GetAll(Pageable pageable);

    Task<Result<TransactionCodeResponse>> GetOne(Guid id);
}

public class TransactionCodeService(ITransactionCodeRepository transactionCodeRepository) : ITransactionCodeService
{
    private readonly ITransactionCodeRepository m_TransactionCodeRepository = transactionCodeRepository;

    public async Task<Result<Page<TransactionCodeResponse>>> GetAll(Pageable pageable)
    {
        var page = await m_TransactionCodeRepository.FindAll(pageable);

        var transactionCodeResponses = page.Items.Select(transactionCode => transactionCode.ToResponse())
                                           .ToList();

        return Result.Ok(new Page<TransactionCodeResponse>(transactionCodeResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<TransactionCodeResponse>> GetOne(Guid id)
    {
        var transactionCode = await m_TransactionCodeRepository.FindById(id);

        if (transactionCode == null)
            return Result.NotFound<TransactionCodeResponse>($"No Transaction Code found with Id: {id}");

        return Result.Ok(transactionCode.ToResponse());
    }
}

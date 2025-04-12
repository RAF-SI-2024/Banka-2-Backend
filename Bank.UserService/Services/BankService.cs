using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IBankService
{
    Task<Result<Page<BankResponse>>> GetAll(BankFilterQuery filter, Pageable pageable);

    Task<Result<BankResponse>> GetOne(Guid id);
}

public class BankService(IBankRepository bankRepository) : IBankService
{
    private readonly IBankRepository m_BankRepository = bankRepository;

    public async Task<Result<Page<BankResponse>>> GetAll(BankFilterQuery filter, Pageable pageable)
    {
        var page = await m_BankRepository.FindAll(filter, pageable);

        var bankResponses = page.Items.Select(bank => bank.ToResponse())
                                .ToList();

        return Result.Ok(new Page<BankResponse>(bankResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<BankResponse>> GetOne(Guid id)
    {
        var bank = await m_BankRepository.FindById(id);

        if (bank is null)
            return Result.NotFound<BankResponse>($"No bank found with Id: {id}");

        return Result.Ok(bank.ToResponse());
    }
}

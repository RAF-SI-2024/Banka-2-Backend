using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

using Result = Bank.Application.Endpoints.Result;

namespace Bank.UserService.Services;

public interface IAccountTypeService
{
    Task<Result<Page<AccountTypeResponse>>> GetAll(AccountTypeFilterQuery accountTypeFilterQuery, Pageable pageable);

    Task<Result<AccountTypeResponse>> GetOne(Guid id);
}

public class AccountTypeService(IAccountTypeRepository accountTypeRepository) : IAccountTypeService
{
    private readonly IAccountTypeRepository m_AccountTypeRepository = accountTypeRepository;

    public async Task<Result<Page<AccountTypeResponse>>> GetAll(AccountTypeFilterQuery accountTypeFilterQuery, Pageable pageable)
    {
        var page = await m_AccountTypeRepository.FindAll(accountTypeFilterQuery, pageable);

        var accountTypeResponses = page.Items.Select(accountType => accountType.ToResponse())
                                       .ToList();

        return Result.Ok(new Page<AccountTypeResponse>(accountTypeResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<AccountTypeResponse>> GetOne(Guid id)
    {
        var accountType = await m_AccountTypeRepository.FindById(id);

        if (accountType is null)
            return Result.NotFound<AccountTypeResponse>($"No Account type found with Id: {id}");

        return Result.Ok(accountType.ToResponse());
    }
}

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ILoanTypeService
{
    Task<Result<Page<LoanTypeResponse>>> GetAll(Pageable pageable);

    Task<Result<LoanTypeResponse>> GetOne(Guid id);

    Task<Result<LoanTypeResponse>> Create(LoanTypeCreateRequest createRequest);

    Task<Result<LoanTypeResponse>> Update(LoanTypeUpdateRequest request, Guid id);
}

public class LoanTypeService(ILoanTypeRepository loanTypeRepository) : ILoanTypeService
{
    private readonly ILoanTypeRepository m_LoanTypeRepository = loanTypeRepository;

    public async Task<Result<Page<LoanTypeResponse>>> GetAll(Pageable pageable)
    {
        var page = await m_LoanTypeRepository.FindAll(pageable);

        var loanTypeResponses = page.Items.Select(lt => lt.ToResponse())
                                    .ToList();

        return Result.Ok(new Page<LoanTypeResponse>(loanTypeResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<LoanTypeResponse>> GetOne(Guid id)
    {
        var loanType = await m_LoanTypeRepository.FindById(id);

        if (loanType == null)
            return Result.NotFound<LoanTypeResponse>($"Loan type with ID {id} not found");

        return Result.Ok(loanType.ToResponse());
    }

    public async Task<Result<LoanTypeResponse>> Create(LoanTypeCreateRequest createRequest)
    {
        var loanType = createRequest.ToLoanType();

        var createdLoanType = await m_LoanTypeRepository.Add(loanType);

        return Result.Ok(createdLoanType.ToResponse());
    }

    public async Task<Result<LoanTypeResponse>> Update(LoanTypeUpdateRequest request, Guid id)
    {
        var dbLoanType = await m_LoanTypeRepository.FindById(id);

        if (dbLoanType == null)
            return Result.NotFound<LoanTypeResponse>($"Loan type with ID {id} not found");

        var loanType = await m_LoanTypeRepository.Update(dbLoanType.Update(request));

        return Result.Ok(loanType.ToResponse());
    }
}

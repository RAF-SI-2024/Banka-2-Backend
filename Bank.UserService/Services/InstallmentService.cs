using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IInstallmentService
{
    Task<Result<InstallmentResponse>> GetOne(Guid id);

    Task<Result<Page<InstallmentResponse>>> GetAllByLoanId(Guid loanId, Pageable pageable);

    Task<Result<InstallmentResponse>> Create(InstallmentRequest request);

    Task<Result<InstallmentResponse>> Update(InstallmentRequest request);
}

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository m_InstallmentRepository;
    private readonly ILoanRepository        m_LoanRepository;

    public InstallmentService(IInstallmentRepository installmentRepository, ILoanRepository loanRepository)
    {
        m_InstallmentRepository = installmentRepository;
        m_LoanRepository        = loanRepository;
    }

    public async Task<Result<InstallmentResponse>> GetOne(Guid id)
    {
        var installment = await m_InstallmentRepository.FindById(id);

        if (installment == null)
            return Result.NotFound<InstallmentResponse>($"Installment with ID {id} not found");

        return Result.Ok(installment.ToResponse());
    }

    public async Task<Result<Page<InstallmentResponse>>> GetAllByLoanId(Guid loanId, Pageable pageable)
    {
        var loan = await m_LoanRepository.FindById(loanId);

        if (loan == null)
            return Result.NotFound<Page<InstallmentResponse>>($"Loan with ID {loanId} not found");

        var page = await m_InstallmentRepository.FindAllByLoanId(loanId, pageable);

        var installmentResponses = page.Items.Select(i => i.ToResponse())
                                       .ToList();

        return Result.Ok(new Page<InstallmentResponse>(installmentResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<InstallmentResponse>> Create(InstallmentRequest request)
    {
        var loan = await m_LoanRepository.FindById(request.LoanId);

        if (loan == null)
            return Result.NotFound<InstallmentResponse>($"Loan with ID {request.LoanId} not found");

        var installment = request.ToInstallment();

        var createdInstallment = await m_InstallmentRepository.Add(installment);

        return Result.Ok(createdInstallment.ToResponse());
    }

    public async Task<Result<InstallmentResponse>> Update(InstallmentRequest request)
    {
        var existingInstallment = await m_InstallmentRepository.FindById(request.InstallmentId);

        if (existingInstallment == null)
            return Result.NotFound<InstallmentResponse>($"Installment with ID {request.InstallmentId} not found");

        if (request.LoanId != existingInstallment.LoanId)
        {
            var loan = await m_LoanRepository.FindById(request.LoanId);

            if (loan == null)
                return Result.NotFound<InstallmentResponse>($"Loan with ID {request.LoanId} not found");
        }

        // TODO: Move this to mapper
        var updatedInstallment = request.ToInstallment();
        updatedInstallment.Id         = request.InstallmentId;
        updatedInstallment.CreatedAt  = existingInstallment.CreatedAt;
        updatedInstallment.ModifiedAt = DateTime.UtcNow;

        var result = await m_InstallmentRepository.Update(existingInstallment, updatedInstallment);

        return Result.Ok(result.ToResponse());
    }
}

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.HostedServices;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IInstallmentService
{
    Task<Result<InstallmentResponse>> GetOne(Guid id);

    Task<Result<Page<InstallmentResponse>>> GetAllByLoanId(Guid loanId, Pageable pageable);

    Task<Result<InstallmentResponse>> Create(InstallmentCreateRequest createRequest);

    Task<Result<InstallmentResponse>> Update(InstallmentUpdateRequest request, Guid id);
}

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository m_InstallmentRepository;
    private readonly LoanHostedService      m_LoanHostedService;
    private readonly ILoanRepository        m_LoanRepository;

    public InstallmentService(IInstallmentRepository installmentRepository, ILoanRepository loanRepository, LoanHostedService loanHostedService)
    {
        m_InstallmentRepository = installmentRepository;
        m_LoanRepository        = loanRepository;
        m_LoanHostedService     = loanHostedService;
    }

    public async Task<Result<InstallmentResponse>> GetOne(Guid id)
    {
        var installment = await m_InstallmentRepository.FindById(id);

        if (installment == null)
            return Result.NotFound<InstallmentResponse>($"Installment with ID {id} not found");

        var loan = await m_LoanRepository.FindById(installment.LoanId);

        if (loan == null)
            return Result.NotFound<InstallmentResponse>($"Loan for Installment with ID {id} not found");

        var response = installment.ToResponse();
        response.Amount = await m_LoanHostedService.CalculateInstallmentAmount(loan);

        return Result.Ok(response);
    }

    public async Task<Result<Page<InstallmentResponse>>> GetAllByLoanId(Guid loanId, Pageable pageable)
    {
        var loan = await m_LoanRepository.FindById(loanId);

        if (loan == null)
            return Result.NotFound<Page<InstallmentResponse>>($"Loan with ID {loanId} not found");

        var page = await m_InstallmentRepository.FindAllByLoanId(loanId, pageable);

        var installmentResponses = new List<InstallmentResponse>();

        foreach (var installment in page.Items)
        {
            var response = installment.ToResponse();
            response.Amount = await m_LoanHostedService.CalculateInstallmentAmount(loan);
            installmentResponses.Add(response);
        }

        return Result.Ok(new Page<InstallmentResponse>(installmentResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<InstallmentResponse>> Create(InstallmentCreateRequest createRequest)
    {
        var loan = await m_LoanRepository.FindById(createRequest.LoanId);

        if (loan == null)
            return Result.NotFound<InstallmentResponse>($"Loan with ID {createRequest.LoanId} not found");

        var installment = createRequest.ToInstallment();

        var createdInstallment = await m_InstallmentRepository.Add(installment);

        return Result.Ok(createdInstallment.ToResponse());
    }

    public async Task<Result<InstallmentResponse>> Update(InstallmentUpdateRequest request, Guid id)
    {
        var dbInstallment = await m_InstallmentRepository.FindById(id);

        if (dbInstallment == null)
            return Result.NotFound<InstallmentResponse>($"Installment with ID {id} not found");

        var installment = await m_InstallmentRepository.Update(dbInstallment, dbInstallment.Update(request));

        return Result.Ok(installment.ToResponse());
    }
}

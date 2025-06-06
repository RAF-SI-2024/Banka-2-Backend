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

public class InstallmentService(IInstallmentRepository installmentRepository, ILoanRepository loanRepository, LoanHostedService loanHostedService) : IInstallmentService
{
    private readonly IInstallmentRepository m_InstallmentRepository = installmentRepository;
    private readonly LoanHostedService      m_LoanHostedService     = loanHostedService;
    private readonly ILoanRepository        m_LoanRepository        = loanRepository;

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

        var installmentAmount = await m_LoanHostedService.CalculateInstallmentAmount(loan);

        var installmentResponses = page.Items.Select(installment =>
                                                     {
                                                         var installmentResponse = installment.ToResponse();

                                                         installmentResponse.Amount = installmentAmount;

                                                         return installmentResponse;
                                                     })
                                       .ToList();

        return Result.Ok(new Page<InstallmentResponse>(installmentResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<InstallmentResponse>> Create(InstallmentCreateRequest createRequest)
    {
        var loan = await m_LoanRepository.FindById(createRequest.LoanId);

        if (loan == null)
            return Result.NotFound<InstallmentResponse>($"Loan with ID {createRequest.LoanId} not found");

        var installment = createRequest.ToInstallment();
        
        var loanResponse = loan.ToLoanResponse();

        var createdInstallment = await m_InstallmentRepository.Add(installment);

        return Result.Ok(createdInstallment.ToResponse(loanResponse));
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

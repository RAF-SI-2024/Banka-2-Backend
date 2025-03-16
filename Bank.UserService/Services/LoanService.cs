using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ILoanService
{
    Task<Result<Page<LoanResponse>>> GetAll(LoanFilterQuery loanFilterQuery, Pageable pageable);

    Task<Result<Page<InstallmentResponse>>> GetAllInstallments(Guid loanId, Pageable pageable);

    Task<Result<LoanResponse>> GetOne(Guid id);

    Task<Result<LoanResponse>> Create(LoanRequest loanRequest);

    Task<Result<LoanResponse>> Update(LoanRequest loanRequest, Guid id);
}

public class LoanService(
    ILoanRepository        loanRepository,
    ILoanTypeRepository    loanTypeRepository,
    IAccountRepository     accountRepository,
    ICurrencyRepository    currencyRepository,
    IInstallmentRepository installmentRepository
) : ILoanService
{
    private readonly ILoanRepository        m_LoanRepository        = loanRepository;
    private readonly ILoanTypeRepository    m_LoanTypeRepository    = loanTypeRepository;
    private readonly IAccountRepository     m_AccountRepository     = accountRepository;
    private readonly ICurrencyRepository    m_CurrencyRepository    = currencyRepository;
    private readonly IInstallmentRepository m_InstallmentRepository = installmentRepository;

    public async Task<Result<Page<LoanResponse>>> GetAll(LoanFilterQuery loanFilterQuery, Pageable pageable)
    {
        var page = await m_LoanRepository.FindAll(loanFilterQuery, pageable);

        var loanResponses = page.Items.Select(loan => loan.ToLoanResponse())
                                .ToList();

        return Result.Ok(new Page<LoanResponse>(loanResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<Page<InstallmentResponse>>> GetAllInstallments(Guid loanId, Pageable pageable)
    {
        var loan = await m_LoanRepository.FindById(loanId);

        if (loan is null)
            return Result.NotFound<Page<InstallmentResponse>>($"No Loan found with Id: {loanId}");

        var page = await m_InstallmentRepository.FindAllByLoanId(loanId, pageable);

        var installmentResponses = page.Items.Select(installment => installment.ToResponse())
                                       .ToList();

        return Result.Ok(new Page<InstallmentResponse>(installmentResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<LoanResponse>> GetOne(Guid id)
    {
        var loan = await m_LoanRepository.FindById(id);

        if (loan is null)
            return Result.NotFound<LoanResponse>($"No Loan found with Id: {id}");

        return Result.Ok(loan.ToLoanResponse());
    }

    public async Task<Result<LoanResponse>> Create(LoanRequest loanRequest)
    {
        var loanType = await m_LoanTypeRepository.FindById(loanRequest.TypeId);
        var account  = await m_AccountRepository.FindById(loanRequest.AccountId);
        var currency = await m_CurrencyRepository.FindById(loanRequest.CurrencyId);

        if (loanType == null || account == null || currency == null)
            return Result.BadRequest<LoanResponse>("Invalid data. Loan type, account, or currency not found.");

        var loan = loanRequest.ToLoan();
        loan = await m_LoanRepository.Add(loan);

        return Result.Ok(loan.ToLoanResponse());
    }

    public async Task<Result<LoanResponse>> Update(LoanRequest loanRequest, Guid id)
    {
        var oldLoan = await m_LoanRepository.FindById(id);

        if (oldLoan is null)
            return Result.NotFound<LoanResponse>($"No Loan found with Id: {id}");

        // Validate that the references exist
        var loanType = await m_LoanTypeRepository.FindById(loanRequest.TypeId);
        var account  = await m_AccountRepository.FindById(loanRequest.AccountId);
        var currency = await m_CurrencyRepository.FindById(loanRequest.CurrencyId);

        if (loanType == null || account == null || currency == null)
            return Result.BadRequest<LoanResponse>("Invalid data. Loan type, account, or currency not found.");

        var updatedLoan = loanRequest.ToLoan();
        updatedLoan.Id           = oldLoan.Id;
        updatedLoan.CreationDate = oldLoan.CreationDate;
        updatedLoan.CreatedAt    = oldLoan.CreatedAt;
        updatedLoan.ModifiedAt   = DateTime.UtcNow;

        var loan = await m_LoanRepository.Update(oldLoan, updatedLoan);

        return Result.Ok(loan.ToLoanResponse());
    }
}

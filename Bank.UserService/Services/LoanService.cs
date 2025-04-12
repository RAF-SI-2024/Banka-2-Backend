using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.HostedServices;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ILoanService
{
    Task<Result<Page<LoanResponse>>> GetAll(LoanFilterQuery loanFilterQuery, Pageable pageable);

    Task<Result<Page<InstallmentResponse>>> GetAllInstallments(Guid loanId, Pageable pageable);

    Task<Result<LoanResponse>> GetOne(Guid id);

    Task<Result<LoanResponse>> Create(LoanCreateRequest loanCreateRequest);

    Task<Result<LoanResponse>> Update(LoanUpdateRequest loanRequest, Guid id);

    Task<Result<Page<LoanResponse>>> GetByClient(Guid clientId, Pageable pageable);
}

public class LoanService(
    ILoanRepository        loanRepository,
    ILoanTypeRepository    loanTypeRepository,
    IAccountRepository     accountRepository,
    ICurrencyRepository    currencyRepository,
    IInstallmentRepository installmentRepository,
    LoanHostedService      loanHostedService
) : ILoanService
{
    private readonly IAccountRepository     m_AccountRepository     = accountRepository;
    private readonly ICurrencyRepository    m_CurrencyRepository    = currencyRepository;
    private readonly IInstallmentRepository m_InstallmentRepository = installmentRepository;
    private readonly LoanHostedService      m_LoanHostedService     = loanHostedService;
    private readonly ILoanRepository        m_LoanRepository        = loanRepository;
    private readonly ILoanTypeRepository    m_LoanTypeRepository    = loanTypeRepository;

    public async Task<Result<Page<LoanResponse>>> GetAll(LoanFilterQuery loanFilterQuery, Pageable pageable)
    {
        var page = await m_LoanRepository.FindAll(loanFilterQuery, pageable);

        var loanResponses = new List<LoanResponse>();

        foreach (var loan in page.Items)
        {
            var response        = loan.ToLoanResponse();
            var currencyForLoan = await m_CurrencyRepository.FindById(loan.CurrencyId);

            if (currencyForLoan == null)
                return Result.NotFound<Page<LoanResponse>>($"No Currency for Loan found with Id: {loan.CurrencyId}");

            var amountInRsd = await m_LoanHostedService.ConvertToRsd(loan.Amount, currencyForLoan);
            var nominalRate = m_LoanHostedService.GetBaseInterestRate(amountInRsd);

            if (loan.InterestType == InterestType.Variable)
                nominalRate = nominalRate + loan.LoanType.Margin;

            response.RemainingAmount        = await m_LoanHostedService.GetRemainingPrincipal(loan, m_InstallmentRepository);
            response.NominalInstallmentRate = nominalRate;

            loanResponses.Add(response);
        }

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

        var response    = loan.ToLoanResponse();
        var amountInRsd = await m_LoanHostedService.ConvertToRsd(loan.Amount, loan.Currency);
        var nominalRate = m_LoanHostedService.GetBaseInterestRate(amountInRsd);

        if (loan.InterestType == InterestType.Variable)
            nominalRate = nominalRate + loan.LoanType.Margin;

        response.RemainingAmount        = await m_LoanHostedService.GetRemainingPrincipal(loan, m_InstallmentRepository);
        response.NominalInstallmentRate = nominalRate;

        return Result.Ok(response);
    }

    public async Task<Result<LoanResponse>> Create(LoanCreateRequest loanCreateRequest)
    {
        var loanType = await m_LoanTypeRepository.FindById(loanCreateRequest.TypeId);
        var account  = await m_AccountRepository.FindById(loanCreateRequest.AccountId);
        var currency = await m_CurrencyRepository.FindById(loanCreateRequest.CurrencyId);

        if (loanType == null || account == null || currency == null)
            return Result.BadRequest<LoanResponse>("Invalid data. Loan type, account, or currency not found.");

        var loan = loanCreateRequest.ToLoan();
        loan = await m_LoanRepository.Add(loan);

        return Result.Ok(loan.ToLoanResponse());
    }

    public async Task<Result<LoanResponse>> Update(LoanUpdateRequest loanRequest, Guid id)
    {
        var dbLoan = await m_LoanRepository.FindById(id);

        if (dbLoan is null)
            return Result.NotFound<LoanResponse>($"No Loan found with Id: {id}");

        var loan        = await m_LoanRepository.Update(dbLoan.Update(loanRequest));
        var amountInRsd = await m_LoanHostedService.ConvertToRsd(loan.Amount, loan.Currency);

        if (loan.Status == LoanStatus.Active)
        {
            var installment = new InstallmentCreateRequest
                              {
                                  InstallmentId   = Guid.NewGuid(),
                                  LoanId          = loan.Id,
                                  InterestRate    = m_LoanHostedService.GetBaseInterestRate(amountInRsd),
                                  ExpectedDueDate = DateOnly.FromDateTime(DateTime.UtcNow.AddMonths(1)),
                                  ActualDueDate   = DateOnly.FromDateTime(DateTime.UtcNow),
                                  Status          = InstallmentStatus.Pending
                              };

            await m_InstallmentRepository.Add(installment.ToInstallment());
        }

        return Result.Ok(loan.ToLoanResponse());
    }

    public async Task<Result<Page<LoanResponse>>> GetByClient(Guid clientId, Pageable pageable)
    {
        var page = await m_LoanRepository.FindByClientId(clientId, pageable);

        if (page.Items.Count == 0)
            return Result.NotFound<Page<LoanResponse>>($"No loans found for Client ID: {clientId}");

        var loanResponses = page.Items.Select(loan => loan.ToLoanResponse())
                                .ToList();

        return Result.Ok(new Page<LoanResponse>(loanResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }
}

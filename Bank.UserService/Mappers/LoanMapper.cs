using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class LoanMapper
{
    public static Loan ToLoan(this LoanCreateRequest createRequest)
    {
        var now = DateTime.UtcNow;

        return new Loan
               {
                   Id           = Guid.NewGuid(),
                   TypeId       = createRequest.TypeId,
                   AccountId    = createRequest.AccountId,
                   Amount       = createRequest.Amount,
                   Period       = createRequest.Period,
                   CreationDate = now,
                   MaturityDate = CalculateMaturityDate(now, createRequest.Period),
                   CurrencyId   = createRequest.CurrencyId,
                   Status       = LoanStatus.Pending,
                   InterestType = createRequest.InterestType,
                   CreatedAt    = now,
                   ModifiedAt   = now
               };
    }

    public static LoanResponse ToLoanResponse(this Loan loan)
    {
        return new LoanResponse
               {
                   Id           = loan.Id,
                   Type         = loan.LoanType!.ToResponse(),
                   Account      = loan.Account!.ToResponse(),
                   Amount       = loan.Amount,
                   Period       = loan.Period,
                   CreationDate = DateOnly.FromDateTime(loan.CreationDate),
                   MaturityDate = DateOnly.FromDateTime(loan.MaturityDate),
                   Currency     = loan.Currency!.ToResponse(),
                   Status       = loan.Status,
                   InterestType = loan.InterestType,
                   CreatedAt    = loan.CreatedAt,
                   ModifiedAt   = loan.ModifiedAt
               };
    }

    public static LoanTypeResponse ToResponse(this LoanType loanType)
    {
        return new LoanTypeResponse
               {
                   Id     = loanType.Id,
                   Name   = loanType.Name,
                   Margin = loanType.Margin
               };
    }

    public static LoanType ToLoanType(this LoanTypeCreateRequest createRequest)
    {
        return new LoanType
               {
                   Id         = Guid.NewGuid(),
                   Name       = createRequest.Name,
                   Margin     = createRequest.Margin,
                   CreatedAt  = DateTime.UtcNow,
                   ModifiedAt = DateTime.UtcNow
               };
    }

    public static Loan Update(this Loan loan, LoanUpdateRequest updateRequest)
    {
        loan.Status     = updateRequest.Status ?? loan.Status;
        loan.ModifiedAt = DateTime.UtcNow;

        return loan;
    }

    public static LoanType Update(this LoanType loanType, LoanTypeUpdateRequest updateRequest)
    {
        loanType.Name       = updateRequest.Name   ?? loanType.Name;
        loanType.Margin     = updateRequest.Margin ?? loanType.Margin;
        loanType.ModifiedAt = DateTime.UtcNow;

        return loanType;
    }

    private static DateTime CalculateMaturityDate(DateTime creationDate, int periodInMonths)
    {
        return creationDate.AddMonths(periodInMonths);
    }
}

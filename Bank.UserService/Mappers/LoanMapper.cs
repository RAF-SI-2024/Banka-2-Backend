using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

public static class LoanMapper
{
    public static Loan ToLoan(this LoanRequest request)
    {
        var now = DateTime.UtcNow;

        return new Loan
               {
                   Id           = Guid.NewGuid(),
                   TypeId       = request.TypeId,
                   AccountId    = request.AccountId,
                   Amount       = request.Amount,
                   Period       = request.Period,
                   CreationDate = now,
                   MaturityDate = CalculateMaturityDate(now, request.Period),
                   CurrencyId   = request.CurrencyId,
                   Status       = LoanStatus.Pending,
                   InterestType = (InterestType)request.InterestType,
                   CreatedAt    = now,
                   ModifiedAt   = now
               };
    }

    public static LoanResponse ToLoanResponse(this Loan loan)
    {
        return new LoanResponse
               {
                   Id           = loan.Id,
                   Type         = loan.LoanType.ToResponse(),
                   Account      = loan.Account.ToResponse(),
                   Amount       = loan.Amount,
                   Period       = loan.Period,
                   CreationDate = DateOnly.FromDateTime(loan.CreationDate),
                   MaturityDate = DateOnly.FromDateTime(loan.MaturityDate),
                   Currency     = loan.Currency.ToResponse(),
                   Status       = (int)loan.Status,
                   InterestType = (int)loan.InterestType,
                   CreatedAt    = loan.CreatedAt,
                   ModifiedAt   = loan.ModifiedAt
               };
    }

    public static LoanTypeResponse ToResponse(this LoanType loanType)
    {
        return new LoanTypeResponse()
               {
                   Id     = loanType.Id,
                   Name   = loanType.Name,
                   Margin = loanType.Margin,
               };
    }

    public static LoanType ToLoanType(this LoanTypeRequest request)
    {
        return new LoanType
               {
                   Id         = Guid.NewGuid(),
                   Name       = request.Name,
                   Margin     = request.Margin,
                   CreatedAt  = DateTime.UtcNow,
                   ModifiedAt = DateTime.UtcNow
               };
    }

    private static DateTime CalculateMaturityDate(DateTime creationDate, int periodInMonths)
    {
        return creationDate.AddMonths(periodInMonths);
    }
}

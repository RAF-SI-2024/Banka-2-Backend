using Bank.Application.Domain;

namespace Bank.Application.Requests;

public class LoanCreateRequest
{
    public Guid         TypeId       { get; set; }
    public Guid         AccountId    { get; set; }
    public decimal      Amount       { get; set; }
    public int          Period       { get; set; }
    public Guid         CurrencyId   { get; set; }
    public InterestType InterestType { get; set; }
}

public class LoanUpdateRequest
{
    public DateTime?   MaturityDate { get; set; }
    public LoanStatus? Status       { get; set; }
}

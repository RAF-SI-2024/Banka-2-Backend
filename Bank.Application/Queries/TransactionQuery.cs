using Bank.Application.Domain;

namespace Bank.Application.Queries;

public class TransactionFilterQuery
{
    public TransactionStatus Status   { set; get; }
    public DateOnly          FromDate { set; get; }
    public DateOnly          ToDate   { set; get; }
}

namespace Bank.UserService.Models;

public class CreditInstallment
{
    public required Guid          Id                { get; set; }
    public required Credit        Credit            { get; set; }
    public required int           CreditNumber      { get; set; }
    public required decimal       InstallmentAmount { get; set; }
    public required decimal       InterestRate      { get; set; }
    public required Currency      Currency          { get; set; }
    public          Guid          CurrencyId        { get; set; }
    public required DateTime      ExpectedDueDate   { get; set; }
    public          DateTime?     ActualDueDate     { get; set; }
    public required PaymentStatus PaymentStatus     { get; set; }
    public          Guid          PaymentStatusId   { get; set; }
    public required DateTime      CreatedAt         { get; set; }
    public required DateTime      ModifiedAt        { get; set; }
}

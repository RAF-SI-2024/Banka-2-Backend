namespace Bank.Application.Queries;

public class CompanyFilterQuery
{
    public string? Name                    { set; get; }
    public string? RegistrationNumber      { set; get; }
    public string? TaxIdentificationNumber { set; get; }
}

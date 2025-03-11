namespace Bank.Application.Requests;

public class CompanyCreateRequest()
{
    public required string Name                    { set; get; }
    public required string RegistrationNumber      { set; get; }
    public required string TaxIdentificationNumber { set; get; }
    public required string ActivityCode            { set; get; }
    public required string Address                 { set; get; }
    public required Guid   MajorityOwnerId         { set; get; }
}

public class CompanyUpdateRequest()
{
    public required string Name            { set; get; }
    public required string ActivityCode    { set; get; }
    public required string Address         { set; get; }
    public required Guid   MajorityOwnerId { set; get; }
}

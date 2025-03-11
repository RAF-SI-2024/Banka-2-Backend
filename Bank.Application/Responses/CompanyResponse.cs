namespace Bank.Application.Responses;

public class CompanyResponse
{
    public required Guid                Id                      { set; get; }
    public required string              Name                    { set; get; }
    public required string              RegistrationNumber      { set; get; }
    public required string              TaxIdentificationNumber { set; get; }
    public required string              ActivityCode            { set; get; }
    public required string              Address                 { set; get; }
    public          UserSimpleResponse? MajorityOwner           { set; get; }
}

public class CompanySimpleResponse
{
    public required Guid   Id                      { set; get; }
    public required string Name                    { set; get; }
    public required string RegistrationNumber      { set; get; }
    public required string TaxIdentificationNumber { set; get; }
    public required string ActivityCode            { set; get; }
    public required string Address                 { set; get; }
}

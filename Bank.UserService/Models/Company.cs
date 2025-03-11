namespace Bank.UserService.Models;

public class Company
{
    public required Guid   Id                      { set; get; }
    public required string Name                    { set; get; }
    public required string RegistrationNumber      { set; get; }
    public required string TaxIdentificationNumber { set; get; }
    public required string ActivityCode            { set; get; }
    public required string Address                 { set; get; }
    public required Guid   MajorityOwnerId         { set; get; }
    public          User?  MajorityOwner           { set; get; }
}

using Bank.Application.Domain;

namespace Bank.Application.Responses;

public class UserResponse
{
    public required Guid                        Id                         { set;  get; }
    public required string                      FirstName                  { set;  get; }
    public required string                      LastName                   { set;  get; }
    public required DateOnly                    DateOfBirth                { set;  get; }
    public required Gender                      Gender                     { set;  get; }
    public required string                      UniqueIdentificationNumber { set;  get; }
    public required string                      Username                   { set;  get; }
    public required string                      Email                      { set;  get; }
    public required string                      PhoneNumber                { set;  get; }
    public required string                      Address                    { set;  get; }
    public required Role                        Role                       { set;  get; }
    public required long                        Permissions                { set;  get; }
    public required string?                     Department                 { set;  get; }
    public required List<AccountSimpleResponse> Accounts                   { init; get; } = [];
    public required DateTime                    CreatedAt                  { set;  get; }
    public required DateTime                    ModifiedAt                 { set;  get; }
    public required bool                        Activated                  { set;  get; }
}

public class UserSimpleResponse
{
    public required Guid     Id                         { set; get; }
    public required string   FirstName                  { set; get; }
    public required string   LastName                   { set; get; }
    public required DateOnly DateOfBirth                { set; get; }
    public required Gender   Gender                     { set; get; }
    public required string   UniqueIdentificationNumber { set; get; }
    public required string   Username                   { set; get; }
    public required string   Email                      { set; get; }
    public required string   PhoneNumber                { set; get; }
    public required string   Address                    { set; get; }
    public required Role     Role                       { set; get; }
    public required long     Permissions                { set; get; }
    public required string?  Department                 { set; get; }
    public required DateTime CreatedAt                  { set; get; }
    public required DateTime ModifiedAt                 { set; get; }
    public required bool     Activated                  { set; get; }
}

public class UserLoginResponse
{
    public required string       Token { set; get; }
    public required UserResponse User  { set; get; }
}

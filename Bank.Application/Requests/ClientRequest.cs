using Bank.Application.Domain;

namespace Bank.Application.Requests;

public class ClientCreateRequest
{
    public required string   FirstName                  { set; get; }
    public required string   LastName                   { set; get; }
    public required DateOnly DateOfBirth                { set; get; }
    public required Gender   Gender                     { set; get; }
    public required string   UniqueIdentificationNumber { set; get; }
    public required string   Email                      { set; get; }
    public required string   PhoneNumber                { set; get; }
    public required string   Address                    { set; get; }
    public required long     Permissions                { set; get; } = (long)Permission.Client;
}

public class ClientUpdateRequest
{
    public required string FirstName   { set; get; }
    public required string LastName    { set; get; }
    public required string PhoneNumber { set; get; }
    public required string Address     { set; get; }
    public required bool   Activated   { set; get; }
}

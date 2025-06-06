using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

file static class Values
{
    public static readonly Guid Id        = Guid.Parse("ae45452a-81fa-413b-9a3f-4e044ff13939");
    public const           bool Activated = true;
}

public static partial class Example
{
    public static class Employee
    {
        public static readonly EmployeeCreateRequest CreateRequest = new()
                                                                     {
                                                                         FirstName                  = "Nikola",
                                                                         LastName                   = "Jovanović",
                                                                         DateOfBirth                = new DateOnly(2005, 5, 17),
                                                                         Gender                     = Gender.Male,
                                                                         UniqueIdentificationNumber = "1705005710032",
                                                                         Username                   = "nikolaj",
                                                                         Email                      = "nikola.jovanovic@example.com",
                                                                         PhoneNumber                = "+381632318592",
                                                                         Address                    = "Kneza Miloša 88",
                                                                         Role                       = Role.Employee,
                                                                         Department                 = "HR",
                                                                         Employed                   = true,
                                                                     };

        public static readonly EmployeeUpdateRequest UpdateRequest = new()
                                                                     {
                                                                         FirstName   = "Update",
                                                                         LastName    = "Update",
                                                                         Activated   = true,
                                                                         Address     = "Kneza Miloša 88",
                                                                         Department  = "HR",
                                                                         Employed    = true,
                                                                         Role        = Role.Employee,
                                                                         Username    = "nikolaj",
                                                                         PhoneNumber = "+381632318592"
                                                                     };

        public static readonly EmployeeResponse Response = new()
                                                           {
                                                               Id                         = Values.Id,
                                                               FirstName                  = CreateRequest.FirstName,
                                                               LastName                   = CreateRequest.LastName,
                                                               DateOfBirth                = CreateRequest.DateOfBirth,
                                                               Gender                     = CreateRequest.Gender,
                                                               UniqueIdentificationNumber = CreateRequest.UniqueIdentificationNumber,
                                                               Username                   = CreateRequest.Username,
                                                               Email                      = CreateRequest.Email,
                                                               PhoneNumber                = CreateRequest.PhoneNumber,
                                                               Address                    = CreateRequest.Address,
                                                               Role                       = CreateRequest.Role,
                                                               Department                 = CreateRequest.Department,
                                                               CreatedAt                  = DateTime.UtcNow,
                                                               ModifiedAt                 = DateTime.UtcNow,
                                                               Employed                   = CreateRequest.Employed,
                                                               Activated                  = Values.Activated,
                                                               Permissions                = 2
                                                           };

        public static readonly EmployeeSimpleResponse SimpleResponse = new()
                                                                       {
                                                                           Id                         = Values.Id,
                                                                           FirstName                  = CreateRequest.FirstName,
                                                                           LastName                   = CreateRequest.LastName,
                                                                           DateOfBirth                = CreateRequest.DateOfBirth,
                                                                           Gender                     = CreateRequest.Gender,
                                                                           UniqueIdentificationNumber = CreateRequest.UniqueIdentificationNumber,
                                                                           Username                   = CreateRequest.Username,
                                                                           Email                      = CreateRequest.Email,
                                                                           PhoneNumber                = CreateRequest.PhoneNumber,
                                                                           Address                    = CreateRequest.Address,
                                                                           Role                       = CreateRequest.Role,
                                                                           Department                 = CreateRequest.Department,
                                                                           CreatedAt                  = DateTime.UtcNow,
                                                                           ModifiedAt                 = DateTime.UtcNow,
                                                                           Employed                   = CreateRequest.Employed,
                                                                           Activated                  = Values.Activated,
                                                                           Permissions                = 2
                                                                       };
    }
}

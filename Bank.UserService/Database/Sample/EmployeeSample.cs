using Bank.Application.Domain;
using Bank.Application.Requests;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
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
                                                                         Permissions                = 1 << 2
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
    }
}

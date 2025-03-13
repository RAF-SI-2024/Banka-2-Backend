using Bank.Application.Domain;
using Bank.Application.Utilities;
using Bank.UserService.Mappers;
using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using EmployeeModel = Employee;

public static partial class Seeder
{
    public static class Employee
    {
        public static readonly EmployeeModel Admin = new()
                                                     {
                                                         Id                         = Guid.Parse("c6f44133-08f2-4a43-bd65-9cfb6b13fa5b"),
                                                         FirstName                  = "Admin",
                                                         LastName                   = "Admin",
                                                         DateOfBirth                = new DateOnly(1990, 1, 1),
                                                         Gender                     = Gender.Male,
                                                         UniqueIdentificationNumber = "0101990710024",
                                                         Email                      = "admin@gmail.com",
                                                         Password                   = "admin",
                                                         Username                   = "Admin123",
                                                         PhoneNumber                = "+38164123456",
                                                         Address                    = "Admin 01",
                                                         Salt                       = Guid.NewGuid(),
                                                         Role                       = Role.Admin,
                                                         Department                 = "Department 01",
                                                         CreatedAt                  = DateTime.UtcNow,
                                                         ModifiedAt                 = DateTime.UtcNow,
                                                         Employed                   = true,
                                                         Activated                  = true
                                                     };

        public static readonly EmployeeModel Employee01 = new()
                                                          {
                                                              Id                         = Guid.Parse("5817c260-e4a9-4dc1-87d9-2fa12af157d9"),
                                                              FirstName                  = "John",
                                                              LastName                   = "Smith",
                                                              DateOfBirth                = new DateOnly(1990, 7, 20),
                                                              Gender                     = Gender.Male,
                                                              UniqueIdentificationNumber = "2007990500012",
                                                              Email                      = "employee1@gmail.com",
                                                              Username                   = "employee1",
                                                              PhoneNumber                = "+38162345678",
                                                              Address                    = "Oak Avenue 45",
                                                              Password                   = "employee1",
                                                              Salt                       = Guid.NewGuid(),
                                                              Role                       = Role.Employee,
                                                              Department                 = "Customer Service",
                                                              CreatedAt                  = DateTime.UtcNow,
                                                              ModifiedAt                 = DateTime.UtcNow,
                                                              Employed                   = true,
                                                              Activated                  = true
                                                          };

        public static readonly EmployeeModel Employee02 = new()
                                                          {
                                                              Id                         = Guid.Parse("3d9bd4c3-a467-4676-ac4b-2b392e7315fa"),
                                                              FirstName                  = "Maria",
                                                              LastName                   = "Jones",
                                                              DateOfBirth                = new DateOnly(1988, 3, 12),
                                                              Gender                     = Gender.Female,
                                                              UniqueIdentificationNumber = "1203988715015",
                                                              Email                      = "employee2@bankapp.com",
                                                              Username                   = "maria.jones",
                                                              PhoneNumber                = "+38163456789",
                                                              Address                    = "Pine Street 78",
                                                              Password                   = "employee2",
                                                              Salt                       = Guid.NewGuid(),
                                                              Role                       = Role.Employee,
                                                              Department                 = "Loans",
                                                              CreatedAt                  = DateTime.UtcNow,
                                                              ModifiedAt                 = DateTime.UtcNow,
                                                              Employed                   = true,
                                                              Activated                  = true,
                                                          };

        public static readonly EmployeeModel Employee03 = new()
                                                          {
                                                              Id                         = Guid.Parse("ffff810e-b416-4b21-88f0-c565ca770192"),
                                                              FirstName                  = "Stefan",
                                                              LastName                   = "Nikolic",
                                                              DateOfBirth                = new DateOnly(1992, 6, 5),
                                                              Gender                     = Gender.Male,
                                                              UniqueIdentificationNumber = "0506992170005",
                                                              Email                      = "employee3@bankapp.com",
                                                              Username                   = "employee3",
                                                              PhoneNumber                = "+38164987654",
                                                              Address                    = "Knez Mihailova 22",
                                                              Password                   = "employee3",
                                                              Salt                       = Guid.NewGuid(),
                                                              Role                       = Role.Employee,
                                                              Department                 = "IT",
                                                              CreatedAt                  = DateTime.UtcNow,
                                                              ModifiedAt                 = DateTime.UtcNow,
                                                              Employed                   = true,
                                                              Activated                  = true,
                                                          };

        public static readonly EmployeeModel Employee04 = new()
                                                          {
                                                              Id                         = Guid.Parse("5d3fca51-26f2-49eb-904d-199f00d71645"),
                                                              FirstName                  = "Ana",
                                                              LastName                   = "Marković",
                                                              DateOfBirth                = new DateOnly(1991, 8, 15),
                                                              Gender                     = Gender.Female,
                                                              UniqueIdentificationNumber = "1508991785013",
                                                              Email                      = "employee4@gmail.com",
                                                              Username                   = "ana.markovic",
                                                              PhoneNumber                = "+38165234567",
                                                              Address                    = "Bulevar Kralja Aleksandra 125",
                                                              Password                   = "employee4",
                                                              Salt                       = Guid.NewGuid(),
                                                              Role                       = Role.Employee,
                                                              Department                 = "Human Resources",
                                                              CreatedAt                  = DateTime.UtcNow,
                                                              ModifiedAt                 = DateTime.UtcNow,
                                                              Employed                   = true,
                                                              Activated                  = true,
                                                          };
    }
}

public static class EmployeeSeederExtension
{
    public static async Task SeedEmployee(this ApplicationContext context)
    {
        if (context.Users.Any(user => user.Role == Role.Admin || user.Role == Role.Employee))
            return;

        EmployeeModel[] employees =
        [
            Seeder.Employee.Admin, Seeder.Employee.Employee01, Seeder.Employee.Employee02, Seeder.Employee.Employee03, Seeder.Employee.Employee04
        ];

        await context.Users.AddRangeAsync(employees.Select(client =>
                                                           {
                                                               client.Password = HashingUtilities.HashPassword(client.Password!, client.Salt);
                                                               return client;
                                                           })
                                                   .Select(client => client.ToUser())
                                                   .ToList());

        await context.SaveChangesAsync();
    }
}

using Bank.Application.Requests;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

namespace Bank.UserService.Test.Examples.Entities;

using EmployeeModel = Employee;

public static partial class Example
{
    public static partial class Entity
    {
        public static class Employee
        {
            public static readonly EmployeeCreateRequest CreateRequest = Database.Examples.Example.Employee.CreateRequest;

            public static readonly EmployeeUpdateRequest UpdateRequest = Database.Examples.Example.Employee.UpdateRequest;

            public static readonly EmployeeModel GetEmployee = new()
                                                               {
                                                                   Id                         = Seeder.Employee.Employee02.Id,
                                                                   FirstName                  = Seeder.Employee.Employee02.FirstName,
                                                                   LastName                   = Seeder.Employee.Employee02.LastName,
                                                                   DateOfBirth                = Seeder.Employee.Employee02.DateOfBirth,
                                                                   Gender                     = Seeder.Employee.Employee02.Gender,
                                                                   UniqueIdentificationNumber = Seeder.Employee.Employee02.UniqueIdentificationNumber,
                                                                   Email                      = Seeder.Employee.Employee02.Email,
                                                                   Username                   = Seeder.Employee.Employee02.Username,
                                                                   PhoneNumber                = Seeder.Employee.Employee02.PhoneNumber,
                                                                   Address                    = Seeder.Employee.Employee02.Address,
                                                                   Password                   = Seeder.Employee.Employee02.Password,
                                                                   Salt                       = Seeder.Employee.Employee02.Salt,
                                                                   Role                       = Seeder.Employee.Employee02.Role,
                                                                   Department                 = Seeder.Employee.Employee02.Department,
                                                                   CreatedAt                  = Seeder.Employee.Employee02.CreatedAt,
                                                                   ModifiedAt                 = Seeder.Employee.Employee02.ModifiedAt,
                                                                   Employed                   = Seeder.Employee.Employee02.Employed,
                                                                   Activated                  = Seeder.Employee.Employee02.Activated,
                                                                   Permissions                = Seeder.Employee.Employee02.Permissions
                                                               };

            public static readonly EmployeeModel UpdateEmployee = new()
                                                                  {
                                                                      Id                         = Seeder.Employee.Employee03.Id,
                                                                      FirstName                  = Seeder.Employee.Employee03.FirstName,
                                                                      LastName                   = Seeder.Employee.Employee03.LastName,
                                                                      DateOfBirth                = Seeder.Employee.Employee03.DateOfBirth,
                                                                      Gender                     = Seeder.Employee.Employee03.Gender,
                                                                      UniqueIdentificationNumber = Seeder.Employee.Employee03.UniqueIdentificationNumber,
                                                                      Email                      = Seeder.Employee.Employee03.Email,
                                                                      Username                   = Seeder.Employee.Employee03.Username,
                                                                      PhoneNumber                = Seeder.Employee.Employee03.PhoneNumber,
                                                                      Address                    = Seeder.Employee.Employee03.Address,
                                                                      Password                   = Seeder.Employee.Employee03.Password,
                                                                      Salt                       = Seeder.Employee.Employee03.Salt,
                                                                      Role                       = Seeder.Employee.Employee03.Role,
                                                                      Department                 = Seeder.Employee.Employee03.Department,
                                                                      CreatedAt                  = Seeder.Employee.Employee03.CreatedAt,
                                                                      ModifiedAt                 = Seeder.Employee.Employee03.ModifiedAt,
                                                                      Employed                   = Seeder.Employee.Employee03.Employed,
                                                                      Activated                  = Seeder.Employee.Employee03.Activated,
                                                                      Permissions                = Seeder.Employee.Employee03.Permissions
                                                                  };
        }
    }
}

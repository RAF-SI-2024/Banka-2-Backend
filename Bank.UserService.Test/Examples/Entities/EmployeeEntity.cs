﻿using Bank.Application.Domain;
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
            public static readonly EmployeeCreateRequest CreateRequest = new()
                                                                         {
                                                                             FirstName                  = "Nikola",
                                                                             LastName                   = "Jovanović",
                                                                             DateOfBirth                = new(2005, 5, 17),
                                                                             Gender                     = Gender.Male,
                                                                             UniqueIdentificationNumber = "1705005710032",
                                                                             Username                   = "nikolaj",
                                                                             Email                      = "nikola.jovanovic@example.com",
                                                                             PhoneNumber                = "+381632318592",
                                                                             Address                    = "Kneza Miloša 88",
                                                                             Role                       = Role.Employee,
                                                                             Department                 = "HR",
                                                                             Employed                   = true
                                                                         };

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
                                                                   Activated                  = Seeder.Employee.Employee02.Activated
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
                                                                      Activated                  = Seeder.Employee.Employee03.Activated
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
}

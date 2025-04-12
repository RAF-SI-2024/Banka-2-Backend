using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.UserService.Database.Sample;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

namespace Bank.UserService.Test.Examples.Entities;

using UserModel = User;

public static partial class Example
{
    public static partial class Entity
    {
        public static class User
        {
            public static readonly UserLoginRequest LoginRequest = Sample.User.LoginRequest;

            public static readonly UserModel GetEmployee = new()
                                                           {
                                                               Id                         = Seeder.Employee.Employee01.Id,
                                                               FirstName                  = Seeder.Employee.Employee01.FirstName,
                                                               LastName                   = Seeder.Employee.Employee01.LastName,
                                                               DateOfBirth                = Seeder.Employee.Employee01.DateOfBirth,
                                                               Gender                     = Seeder.Employee.Employee01.Gender,
                                                               UniqueIdentificationNumber = Seeder.Employee.Employee01.UniqueIdentificationNumber,
                                                               Email                      = Seeder.Employee.Employee01.Email,
                                                               Username                   = Seeder.Employee.Employee01.Username,
                                                               PhoneNumber                = Seeder.Employee.Employee01.PhoneNumber,
                                                               Address                    = Seeder.Employee.Employee01.Address,
                                                               Password                   = Seeder.Employee.Employee01.Password,
                                                               Salt                       = Seeder.Employee.Employee01.Salt,
                                                               Role                       = Seeder.Employee.Employee01.Role,
                                                               BankId                     = Seeder.Bank.Bank02.Id,
                                                               Department                 = Seeder.Employee.Employee01.Department,
                                                               CreatedAt                  = Seeder.Employee.Employee01.CreatedAt,
                                                               ModifiedAt                 = Seeder.Employee.Employee01.ModifiedAt,
                                                               Employed                   = Seeder.Employee.Employee01.Employed,
                                                               Activated                  = Seeder.Employee.Employee01.Activated,
                                                               Permissions                = (long)Permission.Employee
                                                           };

            public static readonly UserModel UpdateEmployee = new()
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
                                                                  BankId                     = Seeder.Bank.Bank02.Id,
                                                                  Department                 = Seeder.Employee.Employee02.Department,
                                                                  CreatedAt                  = Seeder.Employee.Employee02.CreatedAt,
                                                                  ModifiedAt                 = Seeder.Employee.Employee02.ModifiedAt,
                                                                  Employed                   = Seeder.Employee.Employee02.Employed,
                                                                  Activated                  = Seeder.Employee.Employee02.Activated,
                                                                  Permissions                = (long)Permission.Employee
                                                              };

            public static readonly UserActivationRequest UserActivationRequest = Sample.User.ActivationRequest;
        }
    }
}

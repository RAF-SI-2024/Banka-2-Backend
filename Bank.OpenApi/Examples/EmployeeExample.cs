using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Employee
    {
        public static readonly EmployeeCreateRequest DefaultCreateRequest = new()
                                                                            {
                                                                                FirstName                  = Constant.FirstName,
                                                                                LastName                   = Constant.LastName,
                                                                                DateOfBirth                = Constant.CreationDate,
                                                                                Gender                     = Constant.Gender,
                                                                                UniqueIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                                Username                   = Constant.Username,
                                                                                Email                      = Constant.Email,
                                                                                PhoneNumber                = Constant.Phone,
                                                                                Address                    = Constant.Address,
                                                                                Role                       = Constant.Role,
                                                                                Department                 = Constant.Department,
                                                                                Employed                   = Constant.Boolean,
                                                                            };

        public static readonly EmployeeUpdateRequest DefaultUpdateRequest = new()
                                                                            {
                                                                                FirstName   = Constant.FirstName,
                                                                                LastName    = Constant.LastName,
                                                                                Username    = Constant.Username,
                                                                                PhoneNumber = Constant.Phone,
                                                                                Address     = Constant.Address,
                                                                                Role        = Constant.Role,
                                                                                Department  = Constant.Department,
                                                                                Employed    = Constant.Boolean,
                                                                                Activated   = Constant.Boolean,
                                                                            };

        public static readonly EmployeeResponse DefaultResponse = new()
                                                                  {
                                                                      Id                         = Constant.Id,
                                                                      FirstName                  = Constant.FirstName,
                                                                      LastName                   = Constant.LastName,
                                                                      DateOfBirth                = Constant.CreationDate,
                                                                      Gender                     = Constant.Gender,
                                                                      UniqueIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                      Username                   = Constant.Username,
                                                                      Email                      = Constant.Email,
                                                                      PhoneNumber                = Constant.Phone,
                                                                      Address                    = Constant.Address,
                                                                      Role                       = Constant.Role,
                                                                      Permissions                = (long)Permission.Client,
                                                                      Department                 = Constant.Department,
                                                                      Employed                   = Constant.Boolean,
                                                                      Activated                  = Constant.Boolean,
                                                                      CreatedAt                  = Constant.CreatedAt,
                                                                      ModifiedAt                 = Constant.ModifiedAt,
                                                                  };

        public static readonly EmployeeSimpleResponse DefaultSimpleResponse = new()
                                                                              {
                                                                                  Id                         = Constant.Id,
                                                                                  FirstName                  = Constant.FirstName,
                                                                                  LastName                   = Constant.LastName,
                                                                                  DateOfBirth                = Constant.CreationDate,
                                                                                  Gender                     = Constant.Gender,
                                                                                  UniqueIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                                  Username                   = Constant.Username,
                                                                                  Email                      = Constant.Email,
                                                                                  PhoneNumber                = Constant.Phone,
                                                                                  Address                    = Constant.Address,
                                                                                  Role                       = Constant.Role,
                                                                                  Permissions                = (long)Permission.Client,
                                                                                  Department                 = Constant.Department,
                                                                                  Employed                   = Constant.Boolean,
                                                                                  Activated                  = Constant.Boolean,
                                                                                  CreatedAt                  = Constant.CreatedAt,
                                                                                  ModifiedAt                 = Constant.ModifiedAt,
                                                                              };
    }
}

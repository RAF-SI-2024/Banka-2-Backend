using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class Client
    {
        public static readonly ClientCreateRequest DefaultCreateRequest = new()
                                                                          {
                                                                              FirstName                  = Constant.FirstName,
                                                                              LastName                   = Constant.LastName,
                                                                              DateOfBirth                = Constant.CreationDate,
                                                                              Gender                     = Constant.Gender,
                                                                              UniqueIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                              Email                      = Constant.Email,
                                                                              PhoneNumber                = Constant.Phone,
                                                                              Address                    = Constant.Address,
                                                                              Permissions                = Constant.Permissions,
                                                                          };

        public static readonly ClientUpdateRequest DefaultUpdateRequest = new()
                                                                          {
                                                                              FirstName   = Constant.FirstName,
                                                                              LastName    = Constant.LastName,
                                                                              PhoneNumber = Constant.Phone,
                                                                              Address     = Constant.Address,
                                                                              Activated   = Constant.Boolean,
                                                                          };

        public static readonly ClientResponse DefaultResponse = new()
                                                                {
                                                                    Id                         = Constant.Id,
                                                                    FirstName                  = Constant.FirstName,
                                                                    LastName                   = Constant.LastName,
                                                                    DateOfBirth                = Constant.CreationDate,
                                                                    Gender                     = Constant.Gender,
                                                                    UniqueIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                    Email                      = Constant.Email,
                                                                    PhoneNumber                = Constant.Phone,
                                                                    Address                    = Constant.Address,
                                                                    Role                       = Constant.Role,
                                                                    Accounts                   = [Account.DefaultSimpleResponse],
                                                                    Activated                  = Constant.Boolean,
                                                                    CreatedAt                  = Constant.CreatedAt,
                                                                    ModifiedAt                 = Constant.ModifiedAt,
                                                                };

        public static readonly ClientSimpleResponse DefaultSimpleResponse = new()
                                                                            {
                                                                                Id                         = Constant.Id,
                                                                                FirstName                  = Constant.FirstName,
                                                                                LastName                   = Constant.LastName,
                                                                                DateOfBirth                = Constant.CreationDate,
                                                                                Gender                     = Constant.Gender,
                                                                                UniqueIdentificationNumber = Constant.UniqueIdentificationNumber,
                                                                                Email                      = Constant.Email,
                                                                                PhoneNumber                = Constant.Phone,
                                                                                Address                    = Constant.Address,
                                                                                Role                       = Constant.Role,
                                                                                Activated                  = Constant.Boolean,
                                                                                CreatedAt                  = Constant.CreatedAt,
                                                                                ModifiedAt                 = Constant.ModifiedAt,
                                                                            };
    }
}

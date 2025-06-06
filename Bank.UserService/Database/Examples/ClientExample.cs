using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class Client
    {
        public static readonly ClientCreateRequest CreateRequest = new()
                                                                   {
                                                                       FirstName                  = "Aleksandar",
                                                                       LastName                   = "Ivanović",
                                                                       DateOfBirth                = new DateOnly(1995, 7, 12),
                                                                       Gender                     = Gender.Male,
                                                                       UniqueIdentificationNumber = "1207995710029",
                                                                       Email                      = "aleksandar.ivanovic@gmail.com",
                                                                       PhoneNumber                = "+381698812321",
                                                                       Address                    = "Kralja Petra 12",
                                                                   };

        public static readonly ClientUpdateRequest UpdateRequest = new()
                                                                   {
                                                                       FirstName   = "Aleksandar",
                                                                       LastName    = "Ivanović",
                                                                       PhoneNumber = "+381698812321",
                                                                       Address     = "Kralja Petra 12",
                                                                       Activated   = true
                                                                   };

        public static readonly ClientResponse Response = new()
                                                         {
                                                             Id                         = Guid.Parse("f39d319e-db3e-4af5-bada-6bcb908b29e3"),
                                                             FirstName                  = CreateRequest.FirstName,
                                                             LastName                   = CreateRequest.LastName,
                                                             DateOfBirth                = CreateRequest.DateOfBirth,
                                                             Gender                     = CreateRequest.Gender,
                                                             UniqueIdentificationNumber = CreateRequest.UniqueIdentificationNumber,
                                                             Email                      = CreateRequest.Email,
                                                             PhoneNumber                = CreateRequest.PhoneNumber,
                                                             Address                    = CreateRequest.Address,
                                                             Role                       = Role.Client,
                                                             Accounts                   = [],
                                                             CreatedAt                  = DateTime.UtcNow,
                                                             ModifiedAt                 = DateTime.UtcNow,
                                                             Activated                  = UpdateRequest.Activated,
                                                             Permissions                = 1
                                                         };

        public static readonly ClientSimpleResponse SimpleResponse = new()
                                                                     {
                                                                         Id                         = Guid.Parse("f39d319e-db3e-4af5-bada-6bcb908b29e3"),
                                                                         FirstName                  = CreateRequest.FirstName,
                                                                         LastName                   = CreateRequest.LastName,
                                                                         DateOfBirth                = CreateRequest.DateOfBirth,
                                                                         Gender                     = CreateRequest.Gender,
                                                                         UniqueIdentificationNumber = CreateRequest.UniqueIdentificationNumber,
                                                                         Email                      = CreateRequest.Email,
                                                                         PhoneNumber                = CreateRequest.PhoneNumber,
                                                                         Address                    = CreateRequest.Address,
                                                                         Role                       = Role.Client,
                                                                         CreatedAt                  = DateTime.UtcNow,
                                                                         ModifiedAt                 = DateTime.UtcNow,
                                                                         Activated                  = UpdateRequest.Activated,
                                                                         Permissions                = 1
                                                                     };
    }
}

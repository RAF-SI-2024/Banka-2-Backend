using Bank.Application.Domain;
using Bank.Application.Requests;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
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
                                                                       Permissions                = 1 << 3
                                                                   };

        public static readonly ClientUpdateRequest UpdateRequest = new()
                                                                   {
                                                                       FirstName   = "Aleksandar",
                                                                       LastName    = "Ivanović",
                                                                       PhoneNumber = "+381698812321",
                                                                       Address     = "Kralja Petra 12",
                                                                       Activated   = true
                                                                   };
    }
}

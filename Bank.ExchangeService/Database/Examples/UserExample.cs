using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.ExchangeService.Database.Examples;

file static class Values
{
    public static readonly Guid     Id                         = Guid.Parse("73b8f8ee-ff51-4247-b65b-52b8b9a494e5");
    public const           string   FirstName                  = "Marko";
    public const           string   LastName                   = "Petrović";
    public static readonly DateOnly DateOfBirth                = new(1995, 7, 21);
    public static readonly Gender   Gender                     = Gender.Male;
    public const           string   UniqueIdentificationNumber = "2107953710020";
    public const           string   Username                   = "markop";
    public const           string   PhoneNumber                = "+381641234567";
    public const           string   Address                    = "Kraljice Natalije 45";
    public static readonly Role     Role                       = Role.Admin;
    public static readonly string   Department                 = "IT department";
    public static readonly DateTime CreatedAt                  = new(2024, 10, 15, 9, 30, 0);
    public static readonly DateTime ModifiedAt                 = new(2025, 2, 28, 12, 45, 0);
    public const           bool     Activated                  = true;
    public static readonly string   Email                      = "client1@gmail.com";
}

public static partial class Example
{
    public static class User
    {
        public static readonly UserLoginRequest LoginRequest = new()
                                                               {
                                                                   Email    = Values.Email,
                                                                   Password = "client1"
                                                               };

        public static readonly UserActivationRequest ActivationRequest = new()
                                                                         {
                                                                             Password        = "M4rk0Petrovic@2024",
                                                                             ConfirmPassword = "M4rk0Petrovic@2024"
                                                                         };

        public static readonly UserRequestPasswordResetRequest RequestPasswordResetRequest = new()
                                                                                             {
                                                                                                 Email = "marko.petrovic@example.com"
                                                                                             };

        public static readonly UserPasswordResetRequest PasswordResetRequest = new()
                                                                               {
                                                                                   Password        = "M4rk0Petrovic@2025",
                                                                                   ConfirmPassword = "M4rk0Petrovic@2025"
                                                                               };

        public static readonly UserResponse Response = new()
                                                       {
                                                           Id                         = Values.Id,
                                                           FirstName                  = Values.FirstName,
                                                           LastName                   = Values.LastName,
                                                           DateOfBirth                = Values.DateOfBirth,
                                                           Gender                     = Values.Gender,
                                                           UniqueIdentificationNumber = Values.UniqueIdentificationNumber,
                                                           Username                   = Values.Username,
                                                           Email                      = Values.Email,
                                                           PhoneNumber                = Values.PhoneNumber,
                                                           Address                    = Values.Address,
                                                           Role                       = Values.Role,
                                                           Department                 = Values.Department,
                                                           Accounts                   = [],
                                                           CreatedAt                  = Values.CreatedAt,
                                                           ModifiedAt                 = Values.ModifiedAt,
                                                           Activated                  = Values.Activated,
                                                           Permissions                = 4
                                                       };

        public static readonly UserSimpleResponse SimpleResponse = new()
                                                                   {
                                                                       Id                         = Values.Id,
                                                                       FirstName                  = Values.FirstName,
                                                                       LastName                   = Values.LastName,
                                                                       DateOfBirth                = Values.DateOfBirth,
                                                                       Gender                     = Values.Gender,
                                                                       UniqueIdentificationNumber = Values.UniqueIdentificationNumber,
                                                                       Username                   = Values.Username,
                                                                       Email                      = Values.Email,
                                                                       PhoneNumber                = Values.PhoneNumber,
                                                                       Address                    = Values.Address,
                                                                       Role                       = Values.Role,
                                                                       Department                 = Values.Department,
                                                                       CreatedAt                  = Values.CreatedAt,
                                                                       ModifiedAt                 = Values.ModifiedAt,
                                                                       Activated                  = Values.Activated,
                                                                       Permissions                = 4
                                                                   };

        public static readonly UserLoginResponse LoginResponse = new()
                                                                 {
                                                                     Token =
                                                                     "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJleHAiOjE3NDA3ODIyNjAsImlkIjoiNjFlMTY1OTMtM2EyNC00ZDVkLTg3MmMtMTdlMjJhMzQxZDMzIiwicm9sZSI6IkFkbWluIiwiaWF0IjoxNzQwNzgwNDYwLCJuYmYiOjE3NDA3ODA0NjB9.3DsroWriDMpHvuBNOSAiFq8gxdo4TEkc9WK1r2f0Ou0",
                                                                     User = null!
                                                                 };
    }
}

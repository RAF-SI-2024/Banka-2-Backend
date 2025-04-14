using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class User
    {
        public static readonly UserLoginRequest DefaultLoginRequest = new()
                                                                      {
                                                                          Email    = Constant.Email,
                                                                          Password = Constant.Password,
                                                                      };

        public static readonly UserActivationRequest DefaultActivationRequest = new()
                                                                                {
                                                                                    Password        = Constant.Password,
                                                                                    ConfirmPassword = Constant.Password,
                                                                                };

        public static readonly UserRequestPasswordResetRequest DefaultRequestPasswordResetRequest = new()
                                                                                                    {
                                                                                                        Email = Constant.Email,
                                                                                                    };

        public static readonly UserPasswordResetRequest DefaultPasswordResetRequest = new()
                                                                                      {
                                                                                          Password        = Constant.Password,
                                                                                          ConfirmPassword = Constant.Password,
                                                                                      };

        public static readonly UserResponse DefaultResponse = new()
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
                                                                  Department                 = Constant.Department,
                                                                  Accounts                   = [Account.DefaultSimpleResponse],
                                                                  CreatedAt                  = Constant.CreatedAt,
                                                                  ModifiedAt                 = Constant.ModifiedAt,
                                                                  Activated                  = Constant.Boolean,
                                                              };

        public static readonly UserSimpleResponse DefaultSimpleResponse = new()
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
                                                                              Department                 = Constant.Department,
                                                                              CreatedAt                  = Constant.CreatedAt,
                                                                              ModifiedAt                 = Constant.ModifiedAt,
                                                                              Activated                  = Constant.Boolean,
                                                                          };

        public static readonly UserLoginResponse DefaultLoginResponse = new()
                                                                        {
                                                                            Token = Constant.Token,
                                                                            User  = DefaultResponse,
                                                                        };
    }
}

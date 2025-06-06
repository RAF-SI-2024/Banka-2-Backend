using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.Link.Responses;

namespace Bank.Link.Mapper.B3.Content;

public static class ClientMapper
{
    internal static ClientResponse ToNative(this Response.B3.ClientResponse response)
    {
        return new ClientResponse
               {
                   Id                         = Guid.Empty,
                   FirstName                  = response.FirstName,
                   LastName                   = response.LastName,
                   DateOfBirth                = response.BirthDate,
                   Gender                     = Gender.Invalid,
                   UniqueIdentificationNumber = response.Jmbg,
                   Email                      = response.Email,
                   PhoneNumber                = response.Phone,
                   Address                    = response.Address,
                   Role                       = Role.Client,
                   Permissions                = (long)Permission.Client,
                   Accounts                   = [],
                   CreatedAt                  = default,
                   ModifiedAt                 = default,
                   Activated                  = false
               };
    }

    internal static ClientSimpleResponse ToSimpleNative(this Response.B3.ClientResponse response)
    {
        return new ClientSimpleResponse
               {
                   Id                         = Guid.Empty,
                   FirstName                  = response.FirstName,
                   LastName                   = response.LastName,
                   DateOfBirth                = response.BirthDate,
                   Gender                     = Gender.Invalid,
                   UniqueIdentificationNumber = response.Jmbg,
                   Email                      = response.Email,
                   PhoneNumber                = response.Phone,
                   Address                    = response.Address,
                   Role                       = Role.Client,
                   Permissions                = (long)Permission.Client,
                   CreatedAt                  = default,
                   ModifiedAt                 = default,
                   Activated                  = false
               };
    }
}

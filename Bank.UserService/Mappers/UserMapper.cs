using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

namespace Bank.UserService.Mappers;

using Permissions = Permissions.Domain.Permissions;

public static class UserMapper
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse
               {
                   Id                         = user.Id,
                   FirstName                  = user.FirstName,
                   LastName                   = user.LastName,
                   DateOfBirth                = user.DateOfBirth,
                   Gender                     = user.Gender,
                   UniqueIdentificationNumber = user.UniqueIdentificationNumber,
                   Email                      = user.Email,
                   Username                   = user.Username,
                   PhoneNumber                = user.PhoneNumber,
                   Address                    = user.Address,
                   Role                       = user.Role,
                   Permissions                = user.Permissions,
                   Department                 = user.Department,
                   Accounts                   = MapAccounts(user.Accounts),
                   CreatedAt                  = user.CreatedAt,
                   ModifiedAt                 = user.ModifiedAt,
                   Activated                  = user.Activated,
               };
    }

    private static List<AccountSimpleResponse> MapAccounts(List<Account> accounts)
    {
        return accounts.Select(account => account.ToSimpleResponse())
                       .ToList();
    }

    public static UserSimpleResponse ToSimpleResponse(this User user)
    {
        return new UserSimpleResponse
               {
                   Id                         = user.Id,
                   FirstName                  = user.FirstName,
                   LastName                   = user.LastName,
                   DateOfBirth                = user.DateOfBirth,
                   Gender                     = user.Gender,
                   UniqueIdentificationNumber = user.UniqueIdentificationNumber,
                   Email                      = user.Email,
                   Username                   = user.Username,
                   PhoneNumber                = user.PhoneNumber,
                   Address                    = user.Address,
                   Role                       = user.Role,
                   Permissions                = user.Permissions,
                   Department                 = user.Department,
                   CreatedAt                  = user.CreatedAt,
                   ModifiedAt                 = user.ModifiedAt,
                   Activated                  = user.Activated
               };
    }

    public static Employee ToEmployee(this User user)
    {
        return new Employee
               {
                   Id                         = user.Id,
                   FirstName                  = user.FirstName,
                   LastName                   = user.LastName,
                   DateOfBirth                = user.DateOfBirth,
                   Gender                     = user.Gender,
                   UniqueIdentificationNumber = user.UniqueIdentificationNumber,
                   Email                      = user.Email,
                   Username                   = user.Username,
                   PhoneNumber                = user.PhoneNumber,
                   Address                    = user.Address,
                   Password                   = user.Password,
                   Salt                       = user.Salt,
                   Role                       = user.Role,
                   Department                 = user.Department!,
                   CreatedAt                  = user.CreatedAt,
                   ModifiedAt                 = user.ModifiedAt,
                   Employed                   = user.Employed ?? true,
                   Activated                  = user.Activated,
                   Permissions                = user.Permissions
               };
    }

    public static User ToUser(this Employee employee)
    {
        return new User
               {
                   Id                         = employee.Id,
                   FirstName                  = employee.FirstName,
                   LastName                   = employee.LastName,
                   DateOfBirth                = employee.DateOfBirth,
                   Gender                     = employee.Gender,
                   UniqueIdentificationNumber = employee.UniqueIdentificationNumber,
                   Email                      = employee.Email,
                   Username                   = employee.Username,
                   PhoneNumber                = employee.PhoneNumber,
                   Address                    = employee.Address,
                   Password                   = employee.Password,
                   Salt                       = employee.Salt,
                   Role                       = employee.Role,
                   BankId                     = Seeder.Bank.Bank02.Id,
                   Department                 = employee.Department,
                   CreatedAt                  = employee.CreatedAt,
                   ModifiedAt                 = employee.ModifiedAt,
                   Employed                   = employee.Employed,
                   Activated                  = employee.Activated,
                   Permissions                = employee.Permissions
               };
    }

    public static EmployeeResponse ToResponse(this Employee employee)
    {
        return new EmployeeResponse
               {
                   Id                         = employee.Id,
                   FirstName                  = employee.FirstName,
                   LastName                   = employee.LastName,
                   DateOfBirth                = employee.DateOfBirth,
                   Gender                     = employee.Gender,
                   UniqueIdentificationNumber = employee.UniqueIdentificationNumber,
                   Email                      = employee.Email,
                   Username                   = employee.Username,
                   PhoneNumber                = employee.PhoneNumber,
                   Address                    = employee.Address,
                   Role                       = employee.Role,
                   Permissions                = employee.Permissions,
                   Department                 = employee.Department,
                   CreatedAt                  = employee.CreatedAt,
                   ModifiedAt                 = employee.ModifiedAt,
                   Employed                   = employee.Employed,
                   Activated                  = employee.Activated
               };
    }

    public static EmployeeSimpleResponse ToSimpleResponse(this Employee employee)
    {
        return new EmployeeSimpleResponse
               {
                   Id                         = employee.Id,
                   FirstName                  = employee.FirstName,
                   LastName                   = employee.LastName,
                   DateOfBirth                = employee.DateOfBirth,
                   Gender                     = employee.Gender,
                   UniqueIdentificationNumber = employee.UniqueIdentificationNumber,
                   Email                      = employee.Email,
                   Username                   = employee.Username,
                   PhoneNumber                = employee.PhoneNumber,
                   Address                    = employee.Address,
                   Role                       = employee.Role,
                   Permissions                = employee.Permissions,
                   Department                 = employee.Department,
                   CreatedAt                  = employee.CreatedAt,
                   ModifiedAt                 = employee.ModifiedAt,
                   Employed                   = employee.Employed,
                   Activated                  = employee.Activated
               };
    }

    public static Employee ToEmployee(this EmployeeCreateRequest employeeCreateRequest)
    {
        return new Employee
               {
                   FirstName                  = employeeCreateRequest.FirstName,
                   LastName                   = employeeCreateRequest.LastName,
                   DateOfBirth                = employeeCreateRequest.DateOfBirth,
                   Gender                     = employeeCreateRequest.Gender,
                   UniqueIdentificationNumber = employeeCreateRequest.UniqueIdentificationNumber,
                   Email                      = employeeCreateRequest.Email,
                   Username                   = employeeCreateRequest.Username,
                   Salt                       = Guid.NewGuid(),
                   PhoneNumber                = employeeCreateRequest.PhoneNumber,
                   Address                    = employeeCreateRequest.Address,
                   Role                       = employeeCreateRequest.Role,
                   Department                 = employeeCreateRequest.Department,
                   Employed                   = employeeCreateRequest.Employed,
                   Id                         = Guid.NewGuid(),
                   CreatedAt                  = DateTime.UtcNow,
                   ModifiedAt                 = DateTime.UtcNow,
                   Activated                  = false,
                   Permissions                = new Permissions(Permission.Employee)
               };
    }

    public static User Update(this User user, EmployeeUpdateRequest updateRequest)
    {
        user.FirstName   = updateRequest.FirstName;
        user.LastName    = updateRequest.LastName;
        user.Username    = updateRequest.Username;
        user.PhoneNumber = updateRequest.PhoneNumber;
        user.Address     = updateRequest.Address;
        user.Role        = updateRequest.Role;
        user.Department  = updateRequest.Department;
        user.Employed    = updateRequest.Employed;
        user.Activated   = updateRequest.Activated;
        user.ModifiedAt  = DateTime.UtcNow;

        return user;
    }

    public static ClientResponse ToResponse(this Client client)
    {
        return new ClientResponse
               {
                   Id                         = client.Id,
                   FirstName                  = client.FirstName,
                   LastName                   = client.LastName,
                   DateOfBirth                = client.DateOfBirth,
                   Gender                     = client.Gender,
                   UniqueIdentificationNumber = client.UniqueIdentificationNumber,
                   Email                      = client.Email,
                   PhoneNumber                = client.PhoneNumber,
                   Address                    = client.Address,
                   Role                       = client.Role,
                   Permissions                = client.Permissions,
                   Accounts                   = MapAccounts(client.Accounts),
                   CreatedAt                  = client.CreatedAt,
                   ModifiedAt                 = client.ModifiedAt,
                   Activated                  = client.Activated
               };
    }

    public static ClientSimpleResponse ToSimpleResponse(this Client client)
    {
        return new ClientSimpleResponse
               {
                   Id                         = client.Id,
                   FirstName                  = client.FirstName,
                   LastName                   = client.LastName,
                   DateOfBirth                = client.DateOfBirth,
                   Gender                     = client.Gender,
                   UniqueIdentificationNumber = client.UniqueIdentificationNumber,
                   Email                      = client.Email,
                   PhoneNumber                = client.PhoneNumber,
                   Address                    = client.Address,
                   Role                       = client.Role,
                   Permissions                = client.Permissions,
                   CreatedAt                  = client.CreatedAt,
                   ModifiedAt                 = client.ModifiedAt,
                   Activated                  = client.Activated
               };
    }

    public static Client ToClient(this ClientCreateRequest clientCreateRequest)
    {
        return new Client
               {
                   Id                         = Guid.NewGuid(),
                   FirstName                  = clientCreateRequest.FirstName,
                   LastName                   = clientCreateRequest.LastName,
                   DateOfBirth                = clientCreateRequest.DateOfBirth,
                   Gender                     = clientCreateRequest.Gender,
                   UniqueIdentificationNumber = clientCreateRequest.UniqueIdentificationNumber,
                   Email                      = clientCreateRequest.Email,
                   PhoneNumber                = clientCreateRequest.PhoneNumber,
                   Salt                       = Guid.NewGuid(),
                   Address                    = clientCreateRequest.Address,
                   Role                       = Role.Client,
                   BankId                     = Seeder.Bank.Bank02.Id,
                   CreatedAt                  = DateTime.UtcNow,
                   ModifiedAt                 = DateTime.UtcNow,
                   Activated                  = false,
                   Permissions                = new Permissions(Permission.Client)
               };
    }

    public static User Update(this User user, ClientUpdateRequest updateRequest)
    {
        user.FirstName   = updateRequest.FirstName;
        user.LastName    = updateRequest.LastName;
        user.PhoneNumber = updateRequest.PhoneNumber;
        user.Address     = updateRequest.Address;
        user.Activated   = updateRequest.Activated;
        user.ModifiedAt  = DateTime.UtcNow;

        return user;
    }

    public static User Update(this User user, UserUpdatePermissionRequest updateRequest)
    {
        if (updateRequest.Type == PermissionType.Set)
            user.Permissions += updateRequest.Permission;
        else
            user.Permissions -= updateRequest.Permission;

        user.ModifiedAt = DateTime.UtcNow;

        return user;
    }

    public static User ToUser(this Client client)
    {
        return new User
               {
                   Id                         = client.Id,
                   FirstName                  = client.FirstName,
                   LastName                   = client.LastName,
                   DateOfBirth                = client.DateOfBirth,
                   Gender                     = client.Gender,
                   UniqueIdentificationNumber = client.UniqueIdentificationNumber,
                   Email                      = client.Email,
                   Username                   = "",
                   PhoneNumber                = client.PhoneNumber,
                   Address                    = client.Address,
                   Password                   = client.Password,
                   Salt                       = client.Salt,
                   Role                       = client.Role,
                   BankId                     = Seeder.Bank.Bank02.Id,
                   Department                 = null,
                   CreatedAt                  = client.CreatedAt,
                   ModifiedAt                 = client.ModifiedAt,
                   Employed                   = null,
                   Activated                  = client.Activated,
                   Permissions                = client.Permissions
               };
    }

    public static Client ToClient(this User user)
    {
        return new Client
               {
                   Id                         = user.Id,
                   FirstName                  = user.FirstName,
                   LastName                   = user.LastName,
                   DateOfBirth                = user.DateOfBirth,
                   Gender                     = user.Gender,
                   UniqueIdentificationNumber = user.UniqueIdentificationNumber,
                   Email                      = user.Email,
                   PhoneNumber                = user.PhoneNumber,
                   Address                    = user.Address,
                   Password                   = user.Password,
                   Salt                       = user.Salt,
                   Role                       = user.Role,
                   BankId                     = Seeder.Bank.Bank02.Id,
                   CreatedAt                  = user.CreatedAt,
                   ModifiedAt                 = user.ModifiedAt,
                   Activated                  = user.Activated,
                   Permissions                = user.Permissions
               };
    }

    public static User ToUser(this AccountResponse response)
    {
        return new User
               {
                   Id                         = Guid.NewGuid(),
                   FirstName                  = response.Client.FirstName,
                   LastName                   = response.Client.LastName,
                   DateOfBirth                = response.Client.DateOfBirth,
                   Gender                     = response.Client.Gender,
                   UniqueIdentificationNumber = response.Client.UniqueIdentificationNumber,
                   Email                      = response.Client.Email,
                   Username                   = string.Empty,
                   PhoneNumber                = response.Client.PhoneNumber,
                   Address                    = response.Client.Address,
                   Role                       = response.Client.Role,
                   Permissions                = new Permissions(response.Client.Permissions),
                   BankId                     = Seeder.Bank.All.First(bank => bank.Code == response.AccountNumber[..3]).Id,
                   CreatedAt                  = DateTime.UtcNow,
                   ModifiedAt                 = DateTime.UtcNow,
                   Employed                   = false,
                   Activated                  = false
               };
    }
}

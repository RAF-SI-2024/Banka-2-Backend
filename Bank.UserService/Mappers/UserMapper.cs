using Bank.Application.Domain;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Models;

using EmployeeResponse = Bank.Application.Responses.EmployeeResponse;

namespace Bank.UserService.Mappers;

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
                   Department                 = user.Department,
                   Accounts                   = MapAccounts(user.Accounts),
                   CreatedAt                  = user.CreatedAt,
                   ModifiedAt                 = user.ModifiedAt,
                   Activated                  = user.Activated
               };
    }

    private static List<AccountSimpleResponse> MapAccounts(List<Account> accounts) =>
    accounts.Select(account => account.ToSimpleResponse())
            .ToList();

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
                   Activated                  = user.Activated
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
                   Department                 = employee.Department,
                   CreatedAt                  = employee.CreatedAt,
                   ModifiedAt                 = employee.ModifiedAt,
                   Employed                   = employee.Employed,
                   Activated                  = employee.Activated
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
               };
    }

    public static Employee ToEmployee(this EmployeeUpdateRequest employeeUpdateRequest, Employee oldEmployee)
    {
        return new Employee
               {
                   FirstName                  = employeeUpdateRequest.FirstName,
                   LastName                   = employeeUpdateRequest.LastName,
                   Username                   = employeeUpdateRequest.Username,
                   PhoneNumber                = employeeUpdateRequest.PhoneNumber,
                   Address                    = employeeUpdateRequest.Address,
                   Role                       = employeeUpdateRequest.Role,
                   Department                 = employeeUpdateRequest.Department,
                   Employed                   = employeeUpdateRequest.Employed,
                   Activated                  = employeeUpdateRequest.Activated,
                   Id                         = oldEmployee.Id,
                   Password                   = oldEmployee.Password,
                   Salt                       = oldEmployee.Salt,
                   DateOfBirth                = oldEmployee.DateOfBirth,
                   Gender                     = oldEmployee.Gender,
                   UniqueIdentificationNumber = oldEmployee.UniqueIdentificationNumber,
                   Email                      = oldEmployee.Email,
                   CreatedAt                  = oldEmployee.CreatedAt,
                   ModifiedAt                 = DateTime.UtcNow
               };
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
                   CreatedAt                  = DateTime.UtcNow,
                   ModifiedAt                 = DateTime.UtcNow,
                   Activated                  = false
               };
    }

    public static Client ToClient(this ClientUpdateRequest clientUpdateRequest, Client oldClient)
    {
        return new Client
               {
                   Id                         = oldClient.Id,
                   FirstName                  = clientUpdateRequest.FirstName,
                   LastName                   = clientUpdateRequest.LastName,
                   PhoneNumber                = clientUpdateRequest.PhoneNumber,
                   Address                    = clientUpdateRequest.Address,
                   Activated                  = clientUpdateRequest.Activated,
                   DateOfBirth                = oldClient.DateOfBirth,
                   Gender                     = oldClient.Gender,
                   UniqueIdentificationNumber = oldClient.UniqueIdentificationNumber,
                   Email                      = oldClient.Email,
                   Password                   = oldClient.Password,
                   Salt                       = oldClient.Salt,
                   Role                       = oldClient.Role,
                   CreatedAt                  = oldClient.CreatedAt,
                   ModifiedAt                 = DateTime.UtcNow
               };
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
                   Department                 = null,
                   CreatedAt                  = client.CreatedAt,
                   ModifiedAt                 = client.ModifiedAt,
                   Employed                   = null,
                   Activated                  = client.Activated
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
                   CreatedAt                  = user.CreatedAt,
                   ModifiedAt                 = user.ModifiedAt,
                   Activated                  = user.Activated
               };
    }
}

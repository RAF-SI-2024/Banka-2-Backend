using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IEmployeeService
{
    Task<Result<Page<EmployeeResponse>>> GetAll(UserFilterQuery userFilterQuery, Pageable pageable);

    Task<Result<EmployeeResponse>> GetOne(Guid id);

    Task<Result<EmployeeResponse>> Create(EmployeeCreateRequest employeeCreateRequest);

    Task<Result<EmployeeResponse>> Update(EmployeeUpdateRequest employeeUpdateRequest, Guid id);
}

public class EmployeeService(IUserRepository userRepository, IEmailService emailService) : IEmployeeService
{
    private readonly IUserRepository m_UserRepository = userRepository;
    private readonly IEmailService   m_EmailService   = emailService;

    public async Task<Result<Page<EmployeeResponse>>> GetAll(UserFilterQuery userFilterQuery, Pageable pageable)
    {
        userFilterQuery.Role = Role.Employee;

        var page = await m_UserRepository.FindAll(userFilterQuery, pageable);

        var employeeResponses = page.Items.Select(user => user.ToEmployee()
                                                              .ToResponse())
                                    .ToList();

        return Result.Ok(new Page<EmployeeResponse>(employeeResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<EmployeeResponse>> GetOne(Guid id)
    {
        var user = await m_UserRepository.FindById(id);

        if (user == null || user.Role != Role.Employee)
            return Result.NotFound<EmployeeResponse>($"No Employee found with Id: {id}");

        return Result.Ok(user.ToEmployee()
                             .ToResponse());
    }

    public async Task<Result<EmployeeResponse>> Create(EmployeeCreateRequest employeeCreateRequest)
    {
        var user = await m_UserRepository.Add(employeeCreateRequest.ToEmployee()
                                                                   .ToUser());

        await m_EmailService.Send(EmailType.UserActivateAccount, user);

        return Result.Ok(user.ToEmployee()
                             .ToResponse());
    }

    public async Task<Result<EmployeeResponse>> Update(EmployeeUpdateRequest employeeUpdateRequest, Guid id)
    {
        var dbUser = await m_UserRepository.FindById(id);

        if (dbUser is null)
            return Result.NotFound<EmployeeResponse>($"No Employee found with Id: {id}");

        var user = await m_UserRepository.Update(dbUser.Update(employeeUpdateRequest));

        return Result.Ok(user.ToEmployee()
                             .ToResponse());
    }
}

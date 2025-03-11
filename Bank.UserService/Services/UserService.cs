using System.IdentityModel.Tokens.Jwt;

using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.Application.Responses;
using Bank.Application.Utilities;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface IUserService
{
    Task<Result<Page<UserResponse>>> GetAll(UserFilterQuery userFilterQuery, Pageable pageable);

    Task<Result<UserResponse>> GetOne(Guid id);

    Task<Result<UserLoginResponse>> Login(UserLoginRequest userLoginRequest);

    Task<Result> Activate(UserActivationRequest userActivationRequest, string token);

    Task<Result> RequestPasswordReset(UserRequestPasswordResetRequest passwordResetRequest);

    Task<Result> PasswordReset(UserPasswordResetRequest userPasswordResetRequest, string token);
}

public class UserService(IUserRepository userRepository, IEmailService emailService, IAuthorizationService authorizationService) : IUserService
{
    private readonly IUserRepository       m_UserRepository       = userRepository;
    private readonly IEmailService         m_EmailService         = emailService;
    private readonly IAuthorizationService m_AuthorizationService = authorizationService;

    public async Task<Result<Page<UserResponse>>> GetAll(UserFilterQuery userFilterQuery, Pageable pageable)
    {
        var page = await m_UserRepository.FindAll(userFilterQuery, pageable);

        var userResponses = page.Items.Select(user => user.ToResponse())
                                .ToList();

        return Result.Ok(new Page<UserResponse>(userResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<UserResponse>> GetOne(Guid id)
    {
        var user = await m_UserRepository.FindById(id);

        if (user == null)
            return Result.NotFound<UserResponse>();

        return Result.Ok(user.ToResponse());
    }

    public async Task<Result<UserLoginResponse>> Login(UserLoginRequest userLoginRequest)
    {
        var user = await m_UserRepository.FindByEmail(userLoginRequest.Email);

        if (user == null)
            return Result.NotFound<UserLoginResponse>("User with the specified email address was not found.");

        if (!user.Activated)
            return Result.Forbidden<UserLoginResponse>("To proceed, the account has to be activated.");

        if (user.Password != HashingUtilities.HashPassword(userLoginRequest.Password, user.Salt))
            return Result.BadRequest<UserLoginResponse>("The password is incorrect.");

        string token = m_AuthorizationService.GenerateTokenFor(user);

        return Result.Ok(new UserLoginResponse { Token = token, User = user.ToResponse() });
    }

    public async Task<Result> Activate(UserActivationRequest userActivationRequest, string token)
    {
        var handler  = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "id");

        if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out var id))
            return Result.BadRequest("Invalid token");

        if (userActivationRequest.Password != userActivationRequest.ConfirmPassword)
            return Result.BadRequest("Passwords do not match.");

        await m_UserRepository.SetPassword(id, userActivationRequest.Password);

        return Result.Accepted();
    }

    public async Task<Result> RequestPasswordReset(UserRequestPasswordResetRequest passwordResetRequest)
    {
        var user = await m_UserRepository.FindByEmail(passwordResetRequest.Email);

        if (user == null)
            return Result.BadRequest("User with the specified email address was not found.");

        if (!user.Activated)
            return Result.Forbidden("To reset password, the account has to be activated.");

        await m_EmailService.Send(EmailType.UserResetPassword, user);

        return Result.Accepted();
    }

    public async Task<Result> PasswordReset(UserPasswordResetRequest userPasswordResetRequest, string token)
    {
        return await Activate(new UserActivationRequest { Password = userPasswordResetRequest.Password, ConfirmPassword = userPasswordResetRequest.ConfirmPassword }, token);
    }
}

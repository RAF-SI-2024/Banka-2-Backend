namespace Bank.Application.Requests;

public class UserLoginRequest
{
    public required string Email    { set; get; }
    public required string Password { set; get; }
}

public class UserActivationRequest
{
    public required string Password        { set; get; }
    public required string ConfirmPassword { set; get; }
}

public class UserRequestPasswordResetRequest
{
    public required string Email { set; get; }
}

public class UserPasswordResetRequest
{
    public required string Password        { set; get; }
    public required string ConfirmPassword { set; get; }
}

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bank.Application.Endpoints;

public class Result<T>
{
    public readonly T?           Value;
    public readonly string?      Message;
    public readonly ActionResult ActionResult;

    internal Result(ActionResult actionResult, T? value, string? message = null)
    {
        ActionResult = actionResult;
        Value        = value;
        Message      = message;
    }
}

public class Result
{
    public readonly string?      Message;
    public readonly ActionResult ActionResult;

    internal Result(ActionResult actionResult, string? message = null)
    {
        ActionResult = actionResult;
        Message      = message;
    }

    #region Success | Void

    public static Result Ok() => new(new OkResult());

    public static Result Created() => new(new CreatedResult(string.Empty, null));

    public static Result Accepted() => new(new AcceptedResult());

    public static Result NoContent() => new(new NoContentResult());

    #endregion

    #region Success | Object

    public static Result<T> Ok<T>() => new(new OkResult(), default);

    public static Result<T> Ok<T>(T value) => new(new OkObjectResult(value), value);

    public static Result<T> Created<T>() => new(new CreatedResult(string.Empty, null), default);

    public static Result<T> Created<T>(T value) => new(new CreatedResult(string.Empty, value), default);

    public static Result<T> Created<T>(T value, string uri) => new(new CreatedResult(uri, value), default);

    public static Result<T> Created<T>(T value, Uri uri) => new(new CreatedResult(uri, value), default);

    public static Result<T> Accepted<T>() => new(new AcceptedResult(), default);

    public static Result<T> Accepted<T>(T value) => new(new AcceptedResult(string.Empty, value), default);

    public static Result<T> Accepted<T>(T value, string uri) => new(new AcceptedResult(uri, value), default);

    public static Result<T> Accepted<T>(T value, Uri uri) => new(new AcceptedResult(uri, value), default);

    public static Result<T> NoContent<T>() => new(new NoContentResult(), default);

    #endregion

    #region Client Error | Void

    public static Result BadRequest() => new(new BadRequestResult());

    public static Result BadRequest(object value) => new(new BadRequestObjectResult(value));

    public static Result Unauthorized() => new(new UnauthorizedResult());

    public static Result Forbidden() => new(new ForbidResult());

    public static Result Forbidden(object value) => new(new ObjectResult(value) { StatusCode = StatusCodes.Status403Forbidden });

    public static Result Forbidden(params string[] authenticationSchemes) => new(new ForbidResult(authenticationSchemes));

    public static Result Forbidden(AuthenticationProperties properties) => new(new ForbidResult(properties));

    public static Result Forbidden(AuthenticationProperties properties, params string[] authenticationSchemes) => new(new ForbidResult(authenticationSchemes, properties));

    public static Result NotFound() => new(new NotFoundResult());

    public static Result NotFound(object value) => new(new NotFoundObjectResult(value));

    #endregion

    #region Client Error | Object

    public static Result<T> BadRequest<T>() => new(new BadRequestResult(), default);

    public static Result<T> BadRequest<T>(object value) => new(new BadRequestObjectResult(value), default);

    public static Result<T> Unauthorized<T>() => new(new UnauthorizedResult(), default);

    public static Result<T> Forbidden<T>() => new(new ForbidResult(), default);

    public static Result<T> Forbidden<T>(object value) => new(new ObjectResult(value) { StatusCode = StatusCodes.Status403Forbidden }, default);

    public static Result<T> Forbidden<T>(params string[] authenticationSchemes) => new(new ForbidResult(authenticationSchemes), default);

    public static Result<T> Forbidden<T>(AuthenticationProperties properties) => new(new ForbidResult(properties), default);

    public static Result<T> Forbidden<T>(AuthenticationProperties properties, params string[] authenticationSchemes) =>
    new(new ForbidResult(authenticationSchemes, properties), default);

    public static Result<T> NotFound<T>() => new(new NotFoundResult(), default);

    public static Result<T> NotFound<T>(object value) => new(new NotFoundObjectResult(value), default);

    #endregion
}

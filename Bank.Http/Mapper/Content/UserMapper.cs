using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class UserMapper
{
    public static HttpContent ToContent(this UserLoginRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this UserActivationRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this UserRequestPasswordResetRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this UserPasswordResetRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this UserUpdatePermissionRequest request)
    {
        return JsonContent.Create(request);
    }
}

using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class AccountMapper
{
    public static HttpContent ToContent(this AccountCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this AccountUpdateClientRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this AccountUpdateEmployeeRequest request)
    {
        return JsonContent.Create(request);
    }
}

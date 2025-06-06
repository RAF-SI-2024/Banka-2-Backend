using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class AccountTypeMapper
{
    public static HttpContent ToContent(this AccountTypeCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this AccountTypeUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

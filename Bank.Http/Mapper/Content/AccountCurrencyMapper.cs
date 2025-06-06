using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class AccountCurrencyMapper
{
    public static HttpContent ToContent(this AccountCurrencyCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this AccountCurrencyClientUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

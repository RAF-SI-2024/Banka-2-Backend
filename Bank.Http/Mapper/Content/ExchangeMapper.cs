using System.Net.Http.Json;

using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.Http.Mapper.Content;

public static class ExchangeMapper
{
    public static HttpContent ToContent(this ExchangeCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this ExchangeUpdateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this ExchangeMakeExchangeRequest request)
    {
        return JsonContent.Create(request);
    }
}

using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class CardMapper
{
    public static HttpContent ToContent(this CardCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this CardUpdateLimitRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this CardUpdateStatusRequest request)
    {
        return JsonContent.Create(request);
    }
}

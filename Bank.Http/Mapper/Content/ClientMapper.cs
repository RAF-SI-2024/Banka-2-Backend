using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class ClientMapper
{
    public static HttpContent ToContent(this ClientCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this ClientUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

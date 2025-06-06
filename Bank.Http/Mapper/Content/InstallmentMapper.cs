using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class InstallmentMapper
{
    public static HttpContent ToContent(this InstallmentCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this InstallmentUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

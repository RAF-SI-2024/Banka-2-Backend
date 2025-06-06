using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class CompanyMapper
{
    public static HttpContent ToContent(this CompanyCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this CompanyUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

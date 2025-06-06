using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class LoanTypeMapper
{
    public static HttpContent ToContent(this LoanTypeCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this LoanTypeUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

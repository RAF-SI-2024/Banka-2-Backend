using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class LoanMapper
{
    public static HttpContent ToContent(this LoanCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this LoanUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

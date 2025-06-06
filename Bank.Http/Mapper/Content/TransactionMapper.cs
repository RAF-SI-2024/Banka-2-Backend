using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class TransactionMapper
{
    public static HttpContent ToContent(this TransactionCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this TransactionUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class TransactionTemplateMapper
{
    public static HttpContent ToContent(this TransactionTemplateCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this TransactionTemplateUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

using System.Net.Http.Json;

using Bank.Application.Requests;

namespace Bank.Http.Mapper.Content;

public static class EmployeeMapper
{
    public static HttpContent ToContent(this EmployeeCreateRequest request)
    {
        return JsonContent.Create(request);
    }

    public static HttpContent ToContent(this EmployeeUpdateRequest request)
    {
        return JsonContent.Create(request);
    }
}

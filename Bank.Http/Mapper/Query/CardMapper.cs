using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class CardMapper
{
    public static NameValueCollection ToQuery(this CardFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, CardFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this CardFilterQuery query, NameValueCollection collection)
    {
        if (query.Number is not null)
            collection.Add(nameof(query.Number)
                           .ToCamelCase(), query.Number);

        if (query.Name is not null)
            collection.Add(nameof(query.Name)
                           .ToCamelCase(), query.Name);

        return collection;
    }
}

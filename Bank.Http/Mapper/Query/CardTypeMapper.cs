using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class CardTypeMapper
{
    public static NameValueCollection ToQuery(this CardTypeFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, CardTypeFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this CardTypeFilterQuery query, NameValueCollection collection)
    {
        if (query.Name is not null)
            collection.Add(nameof(query.Name)
                           .ToCamelCase(), query.Name);

        return collection;
    }
}

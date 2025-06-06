using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class TransactionCodeMapper
{
    public static NameValueCollection ToQuery(this TransactionCodeFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, TransactionCodeFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this TransactionCodeFilterQuery query, NameValueCollection collection)
    {
        if (query.Code is not null)
            collection.Add(nameof(query.Code)
                           .ToCamelCase(), query.Code);

        return collection;
    }
}

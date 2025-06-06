using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class BankMapper
{
    public static NameValueCollection ToQuery(this BankFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, BankFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this BankFilterQuery query, NameValueCollection collection)
    {
        if (query.Name is not null)
            collection.Add(nameof(query.Name)
                           .ToCamelCase(), query.Name);

        if (query.Code is not null)
            collection.Add(nameof(query.Code)
                           .ToCamelCase(), query.Code);

        return collection;
    }
}

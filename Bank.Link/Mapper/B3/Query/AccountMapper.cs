using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;

namespace Bank.Link.Mapper.B3.Query;

internal static class AccountMapper
{
    internal static NameValueCollection ToQuery(this Queries.Query.B3.AccountFilter query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    internal static NameValueCollection Map(this NameValueCollection collection, Queries.Query.B3.AccountFilter query)
    {
        return MapQuery(query, collection);
    }

    internal static NameValueCollection MapQuery(this Queries.Query.B3.AccountFilter query, NameValueCollection collection)
    {
        collection.Add(nameof(query.AccountNumber)
                       .ToCamelCase(), query.AccountNumber);

        collection.Add(nameof(query.FirstName)
                       .ToCamelCase(), query.FirstName);

        collection.Add(nameof(query.LastName)
                       .ToCamelCase(), query.LastName);

        return collection;
    }
}

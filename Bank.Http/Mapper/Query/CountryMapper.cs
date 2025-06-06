using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class CountryMapper
{
    public static NameValueCollection ToQuery(this CountryFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, CountryFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this CountryFilterQuery query, NameValueCollection collection)
    {
        if (query.Name is not null)
            collection.Add(nameof(query.Name)
                           .ToCamelCase(), query.Name);

        if (query.CurrencyName is not null)
            collection.Add(nameof(query.CurrencyName)
                           .ToCamelCase(), query.CurrencyName);

        if (query.CurrencyCode is not null)
            collection.Add(nameof(query.CurrencyCode)
                           .ToCamelCase(), query.CurrencyCode);

        return collection;
    }
}

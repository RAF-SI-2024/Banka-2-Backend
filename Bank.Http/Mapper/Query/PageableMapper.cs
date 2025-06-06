using System.Collections.Specialized;
using System.Web;

using Bank.Application.Domain;
using Bank.Application.Extensions;

namespace Bank.Http.Mapper.Query;

public static class PageableMapper
{
    public static NameValueCollection ToQuery(this Pageable pageable)
    {
        return MapQuery(pageable, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, Pageable pageable)
    {
        return MapQuery(pageable, collection);
    }

    public static NameValueCollection MapQuery(this Pageable pageable, NameValueCollection collection)
    {
        collection.Add(nameof(pageable.Page)
                       .ToCamelCase(), pageable.Page.ToString());

        collection.Add(nameof(pageable.Size)
                       .ToCamelCase(), pageable.Size.ToString());

        return collection;
    }
}

using System.Collections.Specialized;
using System.Web;

using Bank.Application.Domain;
using Bank.Application.Extensions;

namespace Bank.Link.Mapper.B3.Query;

internal static class PageableMapper
{
    internal static NameValueCollection ToQuery(this Pageable pageable)
    {
        return MapQuery(pageable, HttpUtility.ParseQueryString(string.Empty));
    }

    internal static NameValueCollection Map(this NameValueCollection collection, Pageable pageable)
    {
        return MapQuery(pageable, collection);
    }

    internal static NameValueCollection MapQuery(this Pageable pageable, NameValueCollection collection)
    {
        collection.Add(nameof(pageable.Page)
                       .ToCamelCase(), pageable.Page.ToString());

        collection.Add(nameof(pageable.Size)
                       .ToCamelCase(), pageable.Size.ToString());

        return collection;
    }
}

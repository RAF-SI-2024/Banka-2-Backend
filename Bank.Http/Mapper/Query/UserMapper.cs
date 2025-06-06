using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class UserMapper
{
    public static NameValueCollection ToQuery(this UserFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, UserFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this UserFilterQuery query, NameValueCollection collection)
    {
        if (query.Email is not null)
            collection.Add(nameof(query.Email)
                           .ToCamelCase(), query.Email);

        if (query.FirstName is not null)
            collection.Add(nameof(query.FirstName)
                           .ToCamelCase(), query.FirstName);

        if (query.LastName is not null)
            collection.Add(nameof(query.LastName)
                           .ToCamelCase(), query.LastName);

        collection.Add(nameof(query.Role)
                       .ToCamelCase(), ((int)query.Role).ToString());

        if (query.Permissions is not 0)
            collection.Add(nameof(query.Permissions)
                           .ToCamelCase(), query.Permissions.ToString());

        if (query.Ids.Count > 0)
            foreach (var id in query.Ids)
                collection.Add(nameof(query.Ids)
                               .ToCamelCase(), id.ToString());

        return collection;
    }
}

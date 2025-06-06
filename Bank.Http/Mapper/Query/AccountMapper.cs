using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class AccountMapper
{
    public static NameValueCollection ToQuery(this AccountFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, AccountFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this AccountFilterQuery query, NameValueCollection collection)
    {
        if (query.Number is not null)
            collection.Add(nameof(query.Number)
                           .ToCamelCase(), query.Number);

        if (query.ClientEmail is not null)
            collection.Add(nameof(query.ClientEmail)
                           .ToCamelCase(), query.ClientEmail);

        if (query.ClientLastName is not null)
            collection.Add(nameof(query.ClientLastName)
                           .ToCamelCase(), query.ClientLastName);

        if (query.ClientFirstName is not null)
            collection.Add(nameof(query.ClientFirstName)
                           .ToCamelCase(), query.ClientFirstName);

        if (query.EmployeeEmail is not null)
            collection.Add(nameof(query.EmployeeEmail)
                           .ToCamelCase(), query.EmployeeEmail);

        if (query.CurrencyName is not null)
            collection.Add(nameof(query.CurrencyName)
                           .ToCamelCase(), query.CurrencyName);

        if (query.AccountTypeName is not null)
            collection.Add(nameof(query.AccountTypeName)
                           .ToCamelCase(), query.AccountTypeName);

        if (query.Status is not null)
            collection.Add(nameof(query.Status)
                           .ToCamelCase(), query.Status.ToString());

        if (query.Ids.Count > 0)
            foreach (var id in query.Ids)
                collection.Add(nameof(query.Ids)
                               .ToCamelCase(), id.ToString());

        return collection;
    }
}

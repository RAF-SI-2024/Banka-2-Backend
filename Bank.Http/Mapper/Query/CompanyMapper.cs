using System.Collections.Specialized;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class CompanyMapper
{
    public static NameValueCollection ToQuery(this CompanyFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, CompanyFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this CompanyFilterQuery query, NameValueCollection collection)
    {
        if (query.Name is not null)
            collection.Add(nameof(query.Name)
                           .ToCamelCase(), query.Name);

        if (query.RegistrationNumber is not null)
            collection.Add(nameof(query.RegistrationNumber)
                           .ToCamelCase(), query.RegistrationNumber);

        if (query.TaxIdentificationNumber is not null)
            collection.Add(nameof(query.TaxIdentificationNumber)
                           .ToCamelCase(), query.TaxIdentificationNumber);

        return collection;
    }
}

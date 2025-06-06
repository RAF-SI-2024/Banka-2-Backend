using System.Collections.Specialized;
using System.Globalization;
using System.Web;

using Bank.Application.Extensions;
using Bank.Application.Queries;

namespace Bank.Http.Mapper.Query;

public static class LoanMapper
{
    public static NameValueCollection ToQuery(this LoanFilterQuery query)
    {
        return MapQuery(query, HttpUtility.ParseQueryString(string.Empty));
    }

    public static NameValueCollection Map(this NameValueCollection collection, LoanFilterQuery query)
    {
        return MapQuery(query, collection);
    }

    public static NameValueCollection MapQuery(this LoanFilterQuery query, NameValueCollection collection)
    {
        if (query.LoanTypeId is not null)
            collection.Add(nameof(query.LoanTypeId)
                           .ToCamelCase(), query.LoanTypeId.ToString());

        if (query.AccountNumber is not null)
            collection.Add(nameof(query.AccountNumber)
                           .ToCamelCase(), query.AccountNumber);

        if (query.LoanStatus is not null)
            collection.Add(nameof(query.LoanStatus)
                           .ToCamelCase(), query.LoanStatus);

        if (query.FromDate is not null)
            collection.Add(nameof(query.FromDate)
                           .ToCamelCase(), query.FromDate.Value.ToString(CultureInfo.InvariantCulture));

        if (query.ToDate is not null)
            collection.Add(nameof(query.ToDate)
                           .ToCamelCase(), query.ToDate.Value.ToString(CultureInfo.InvariantCulture));

        return collection;
    }
}

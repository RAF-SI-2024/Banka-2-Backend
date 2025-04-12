using Bank.Application.Requests;
using Bank.UserService.Database.Sample;

namespace Bank.UserService.Test.Examples.Entities;

public static partial class Example
{
    public static partial class Entity
    {
        public static class LoanType
        {
            public static readonly LoanTypeCreateRequest       Request       = Sample.LoanType.Request;
            public static readonly LoanTypeUpdateRequest UpdateRequest = Sample.LoanType.UpdateRequest;
        }
    }
}

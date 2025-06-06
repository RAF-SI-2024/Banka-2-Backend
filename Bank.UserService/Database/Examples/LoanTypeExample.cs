using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.UserService.Database.Examples;

public static partial class Example
{
    public static class LoanType
    {
        public static readonly LoanTypeCreateRequest CreateRequest = new()
                                                                     {
                                                                         Name   = "Lični kredit",
                                                                         Margin = 3.5m,
                                                                     };

        public static readonly LoanTypeUpdateRequest UpdateRequest = new()
                                                                     {
                                                                         Name   = "Lični kredit",
                                                                         Margin = 4m,
                                                                     };

        public static readonly LoanTypeResponse Response = new()
                                                           {
                                                               Id     = Guid.Parse("74358a7f-2b43-4839-a1f8-f48b7fc952e5"),
                                                               Name   = CreateRequest.Name,
                                                               Margin = CreateRequest.Margin
                                                           };
    }
}

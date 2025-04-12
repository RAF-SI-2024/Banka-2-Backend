using Bank.Application.Requests;

namespace Bank.UserService.Database.Sample;

public static partial class Sample
{
    public static class LoanType
    {
        public static readonly LoanTypeCreateRequest Request = new()
                                                         {
                                                             Name   = "Lični kredit",
                                                             Margin = 3.5m,
                                                         };

        public static readonly LoanTypeUpdateRequest UpdateRequest = new()
                                                                     {
                                                                         Name   = "Lični kredit",
                                                                         Margin = 4m,
                                                                     };
    }
}

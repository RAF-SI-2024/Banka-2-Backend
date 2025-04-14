using Bank.Application.Requests;
using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public static class LoanType
    {
        public static readonly LoanTypeCreateRequest DefaultCreateRequest = new()
                                                                            {
                                                                                Name   = Constant.LoanTypeName,
                                                                                Margin = Constant.LoanTypeMargin,
                                                                            };

        public static readonly LoanTypeUpdateRequest DefaultUpdateRequest = new()
                                                                            {
                                                                                Name   = Constant.LoanTypeName,
                                                                                Margin = Constant.LoanTypeMargin,
                                                                            };

        public static readonly LoanTypeResponse DefaultResponse = new()
                                                                  {
                                                                      Id     = Constant.Id,
                                                                      Name   = Constant.LoanTypeName,
                                                                      Margin = Constant.LoanTypeMargin
                                                                  };
    }
}

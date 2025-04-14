using Bank.Application.Responses;

namespace Bank.OpenApi.Examples;

public static partial class Example
{
    public class Country
    {
        public static readonly CountryResponse DefaultResponse = new()
                                                                 {
                                                                     Id         = Constant.Id,
                                                                     Name       = Constant.CountryName,
                                                                     Currency   = Currency.DefaultSimpleResponse,
                                                                     CreatedAt  = Constant.CreatedAt,
                                                                     ModifiedAt = Constant.ModifiedAt,
                                                                 };

        public static readonly CountrySimpleResponse DefaultSimpleResponse = new()
                                                                             {
                                                                                 Id         = Constant.Id,
                                                                                 Name       = Constant.CountryName,
                                                                                 CreatedAt  = Constant.CreatedAt,
                                                                                 ModifiedAt = Constant.ModifiedAt,
                                                                             };
    }
}

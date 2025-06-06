using System.Collections.Immutable;

using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using CardTypeModel = CardType;

public static partial class Seeder
{
    public static class CardType
    {
        public static readonly CardTypeModel VisaDebitCard = new()
                                                             {
                                                                 Id         = Guid.Parse("cd2ea450-14f3-4c46-a35a-7dccf783f48a"),
                                                                 Name       = "Visa Debit",
                                                                 CreatedAt  = DateTime.UtcNow,
                                                                 ModifiedAt = DateTime.UtcNow
                                                             };

        public static readonly CardTypeModel MasterCardGold = new()
                                                              {
                                                                  Id         = Guid.Parse("57c343fb-6e7d-4076-841b-1364fea2fe26"),
                                                                  Name       = "MasterCard Gold",
                                                                  CreatedAt  = DateTime.UtcNow,
                                                                  ModifiedAt = DateTime.UtcNow
                                                              };

        public static readonly CardTypeModel DinaCardStandard = new()
                                                                {
                                                                    Id         = Guid.Parse("c138f49f-4ac8-4604-b157-66047d4177e7"),
                                                                    Name       = "DinaCard Standard",
                                                                    CreatedAt  = DateTime.UtcNow,
                                                                    ModifiedAt = DateTime.UtcNow
                                                                };

        public static readonly CardTypeModel VisaBusinessCard = new()
                                                                {
                                                                    Id         = Guid.Parse("60b147f8-5469-480a-8499-cdcad1880e1e"),
                                                                    Name       = "Visa Business",
                                                                    CreatedAt  = DateTime.UtcNow,
                                                                    ModifiedAt = DateTime.UtcNow
                                                                };

        public static readonly CardTypeModel AmericanExpressPlatinumCard = new()
                                                                           {
                                                                               Id         = Guid.Parse("c22cc01b-b0d8-4712-8d68-e5191b5cdc1a"),
                                                                               Name       = "American Express Platinum",
                                                                               CreatedAt  = DateTime.UtcNow,
                                                                               ModifiedAt = DateTime.UtcNow
                                                                           };

        public static readonly ImmutableArray<CardTypeModel> All =
        [
            VisaDebitCard, MasterCardGold, DinaCardStandard, VisaBusinessCard, AmericanExpressPlatinumCard
        ];
    }
}

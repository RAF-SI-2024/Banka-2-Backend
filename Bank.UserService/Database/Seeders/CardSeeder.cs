using Bank.UserService.Models;

namespace Bank.UserService.Database.Seeders;

using CardModel = Card;

public static partial class Seeder
{
    public static class Card
    {
        public static readonly CardModel Card01 = new()
                                                  {
                                                      Id         = Guid.Parse("98265fe8-5f6a-49de-9b4d-914ef7116c52"),
                                                      Number     = "4111111111111111",
                                                      TypeId     = CardType.VisaDebitCard.Id,
                                                      Name       = "Peter Parker Personal Card",
                                                      ExpiresAt  = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(5)),
                                                      AccountId  = Account.DomesticAccount01.Id,
                                                      CVV        = "123",
                                                      Limit      = 10000.00m,
                                                      Status     = true,
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };

        public static readonly CardModel Card02 = new()
                                                  {
                                                      Id         = Guid.Parse("08711cd1-1960-45bc-aaaa-ef1779ac322b"),
                                                      Number     = "5111111111111118",
                                                      TypeId     = CardType.MasterCardGold.Id,
                                                      Name       = "Mary Watson Premium Card",
                                                      ExpiresAt  = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(5)),
                                                      AccountId  = Account.DomesticAccount02.Id,
                                                      CVV        = "456",
                                                      Limit      = 15000.00m,
                                                      Status     = true,
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };

        public static readonly CardModel Card03 = new()
                                                  {
                                                      Id         = Guid.Parse("4d18a4c9-8f48-4044-9424-625b49106b36"),
                                                      Number     = "9891111111111116",
                                                      TypeId     = CardType.DinaCardStandard.Id,
                                                      Name       = "Mary Watson Secondary Card",
                                                      ExpiresAt  = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(5)),
                                                      AccountId  = Account.ForeignAccount01.Id,
                                                      CVV        = "789",
                                                      Limit      = 5000.00m,
                                                      Status     = true,
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };

        public static readonly CardModel Card04 = new()
                                                  {
                                                      Id         = Guid.Parse("76f410a1-10f8-472c-9ffc-a78cc0d654ac"),
                                                      Number     = "4111222233334444",
                                                      TypeId     = CardType.VisaBusinessCard.Id,
                                                      Name       = "Marko Jovanović Business Card",
                                                      ExpiresAt  = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(5)),
                                                      AccountId  = Account.ForeignAccount02.Id,
                                                      CVV        = "321",
                                                      Limit      = 25000.00m,
                                                      Status     = true,
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };

        public static readonly CardModel Card05 = new()
                                                  {
                                                      Id         = Guid.Parse("9ac8217c-0edc-412a-99df-57f29572fdbb"),
                                                      Number     = "371122223333444",
                                                      TypeId     = CardType.AmericanExpressPlatinumCard.Id,
                                                      Name       = "Jelena Petrović Platinum Card",
                                                      ExpiresAt  = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(5)),
                                                      AccountId  = Account.ForeignAccount03.Id,
                                                      CVV        = "4567",
                                                      Limit      = 50000.00m,
                                                      Status     = true,
                                                      CreatedAt  = DateTime.UtcNow,
                                                      ModifiedAt = DateTime.UtcNow
                                                  };
    }
}

public static class CardSeederExtension
{
    public static async Task SeedCard(this ApplicationContext context)
    {
        if (context.Cards.Any())
            return;

        await context.Cards.AddRangeAsync([
                                              Seeder.Card.Card01, Seeder.Card.Card02, Seeder.Card.Card03, Seeder.Card.Card04, Seeder.Card.Card05
                                          ]);

        await context.SaveChangesAsync();
    }
}

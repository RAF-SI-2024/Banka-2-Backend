using Bank.ExchangeService.Models;

namespace Bank.ExchangeService.Database.Seeders;

using AssetModel = Asset;

public static partial class Seeder
{
    public static class Asset
    {
        public static readonly AssetModel Asset1 = new()
                                                   {
                                                       Id           = Guid.Parse("bc133e0a-8082-4b34-bf3b-909597b6beb3"),
                                                       ActuaryId    = Guid.Parse("5817c260-e4a9-4dc1-87d9-2fa12af157d9"),
                                                       SecurityId   = Option.TeslaPutOption.Id,
                                                       Security     = Option.TeslaPutOption,
                                                       Quantity     = 10,
                                                       AveragePrice = 2000,
                                                       CreatedAt    = DateTime.UtcNow,
                                                       ModifiedAt   = DateTime.UtcNow
                                                   };

    }
}

public static class AssetSeederExtension
{
    public static async Task SeedAssetsHardcoded(this DatabaseContext context)
    {
        if (context.Assets.Any())
            return;

        await context.Assets.AddRangeAsync(Seeder.Asset.Asset1);

        await context.SaveChangesAsync();
    }
}
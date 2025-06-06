using Bank.Application.Domain;
using Bank.Application.Responses;
using Bank.ExchangeService.Database.Seeders;

namespace Bank.ExchangeService.Database.Examples;

public static partial class Example
{
    public static class FutureContract
    {
        public static readonly FutureContractSimpleResponse SimpleResponse = new()
                                                                             {
                                                                                 Id                           = Seeder.FutureContract.GoldFuture.Id,
                                                                                 ContractSize                 = Seeder.FutureContract.GoldFuture.ContractSize,
                                                                                 ContractUnit                 = (ContractUnit)Seeder.FutureContract.GoldFuture.ContractUnit!,
                                                                                 SettlementDate               = Seeder.FutureContract.GoldFuture.SettlementDate,
                                                                                 Name                         = Seeder.FutureContract.GoldFuture.Name,
                                                                                 Ticker                       = Seeder.FutureContract.GoldFuture.Ticker,
                                                                                 HighPrice                    = Seeder.FutureContract.GoldFuture.HighPrice,
                                                                                 LowPrice                     = Seeder.FutureContract.GoldFuture.LowPrice,
                                                                                 Volume                       = Seeder.FutureContract.GoldFuture.Volume,
                                                                                 PriceChangeInInterval        = Seeder.FutureContract.GoldFuture.PriceChange,
                                                                                 PriceChangePercentInInterval = Seeder.FutureContract.GoldFuture.PriceChangePercent,
                                                                                 AskPrice                     = Seeder.FutureContract.GoldFuture.AskPrice,
                                                                                 CreatedAt                    = DateTime.UtcNow,
                                                                                 ModifiedAt                   = DateTime.UtcNow,
                                                                                 BidPrice                     = 60,
                                                                                 AskSize                      = 40,
                                                                                 BidSize                      = 80,
                                                                                 ContractCount                = 1
                                                                             };

        public static readonly FutureContractResponse Response = new()
                                                                 {
                                                                     Id                           = Seeder.FutureContract.GoldFuture.Id,
                                                                     ContractSize                 = Seeder.FutureContract.GoldFuture.ContractSize,
                                                                     ContractUnit                 = (ContractUnit)Seeder.FutureContract.GoldFuture.ContractUnit,
                                                                     SettlementDate               = Seeder.FutureContract.GoldFuture.SettlementDate,
                                                                     Name                         = Seeder.FutureContract.GoldFuture.Name,
                                                                     Ticker                       = Seeder.FutureContract.GoldFuture.Ticker,
                                                                     HighPrice                    = Seeder.FutureContract.GoldFuture.HighPrice,
                                                                     LowPrice                     = Seeder.FutureContract.GoldFuture.LowPrice,
                                                                     AskPrice                     = Seeder.FutureContract.GoldFuture.AskPrice,
                                                                     BidPrice                     = Seeder.FutureContract.GoldFuture.BidPrice,
                                                                     Volume                       = Seeder.FutureContract.GoldFuture.Volume,
                                                                     PriceChangeInInterval        = Seeder.FutureContract.GoldFuture.PriceChange,
                                                                     PriceChangePercentInInterval = Seeder.FutureContract.GoldFuture.PriceChangePercent,
                                                                     CreatedAt                    = DateTime.UtcNow,
                                                                     ModifiedAt                   = DateTime.UtcNow,
                                                                     StockExchange                = null!,
                                                                     Quotes                       = [],
                                                                     AskSize                      = 40,
                                                                     BidSize                      = 80,
                                                                     ContractCount                = 1
                                                                 };

        public static readonly FutureContractDailyResponse DailyResponse = new()
                                                                           {
                                                                               Id                           = Seeder.FutureContract.GoldFuture.Id,
                                                                               ContractSize                 = Seeder.FutureContract.GoldFuture.ContractSize,
                                                                               ContractUnit                 = (ContractUnit)Seeder.FutureContract.GoldFuture.ContractUnit,
                                                                               SettlementDate               = Seeder.FutureContract.GoldFuture.SettlementDate,
                                                                               Name                         = Seeder.FutureContract.GoldFuture.Name,
                                                                               Ticker                       = Seeder.FutureContract.GoldFuture.Ticker,
                                                                               HighPrice                    = Seeder.FutureContract.GoldFuture.HighPrice,
                                                                               LowPrice                     = Seeder.FutureContract.GoldFuture.LowPrice,
                                                                               OpenPrice                    = Seeder.FutureContract.GoldFuture.OpeningPrice,
                                                                               ClosePrice                   = Seeder.FutureContract.GoldFuture.ClosePrice,
                                                                               Volume                       = Seeder.FutureContract.GoldFuture.Volume,
                                                                               PriceChangeInInterval        = Seeder.FutureContract.GoldFuture.PriceChange,
                                                                               PriceChangePercentInInterval = Seeder.FutureContract.GoldFuture.PriceChangePercent,
                                                                               CreatedAt                    = DateTime.UtcNow,
                                                                               ModifiedAt                   = DateTime.UtcNow,
                                                                               StockExchange                = null!,
                                                                               Quotes                       = []
                                                                           };
    }
}

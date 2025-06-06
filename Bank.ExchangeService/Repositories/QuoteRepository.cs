using Bank.Database.Core;
using Bank.ExchangeService.Models;

using DatabaseContext = Bank.ExchangeService.Database.DatabaseContext;

namespace Bank.ExchangeService.Repositories;

public interface IQuoteRepository
{
    Task<bool> CreateQuotes(List<Quote> quotes);
}

public class QuoteRepository(IDatabaseContextFactory<DatabaseContext> contextFactory) : IQuoteRepository
{
    private readonly IDatabaseContextFactory<DatabaseContext> m_ContextFactory = contextFactory;

    public async Task<bool> CreateQuotes(List<Quote> quotes)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.Quotes.AddRangeAsync(quotes.Select(quote => new Quote
                                                                  {
                                                                      Id                = quote.Id,
                                                                      SecurityId        = quote.SecurityId,
                                                                      AskPrice          = quote.AskPrice,
                                                                      BidPrice          = quote.BidPrice,
                                                                      AskSize           = quote.AskSize,
                                                                      BidSize           = quote.BidSize,
                                                                      HighPrice         = quote.HighPrice,
                                                                      LowPrice          = quote.LowPrice,
                                                                      ClosePrice        = quote.ClosePrice,
                                                                      OpeningPrice      = quote.OpeningPrice,
                                                                      Volume            = quote.Volume,
                                                                      ContractCount     = quote.ContractCount,
                                                                      ImpliedVolatility = quote.ImpliedVolatility,
                                                                      CreatedAt         = quote.CreatedAt,
                                                                      ModifiedAt        = quote.ModifiedAt
                                                                  }));

        return await context.SaveChangesAsync() == quotes.Count;
    }
}

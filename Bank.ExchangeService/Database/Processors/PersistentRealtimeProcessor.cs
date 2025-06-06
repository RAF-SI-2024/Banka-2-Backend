using System.Diagnostics;

using Bank.ExchangeService.Configurations;
using Bank.ExchangeService.Models;
using Bank.ExchangeService.Repositories;

namespace Bank.ExchangeService.Database.Processors;

public class PersistentRealtimeProcessor(IQuoteRepository quoteRepository) : IRealtimeProcessor
{
    private readonly IQuoteRepository m_QuoteRepository = quoteRepository;

    private readonly Stopwatch m_StockTimer  = Stopwatch.StartNew();
    private readonly Stopwatch m_ForexTimer  = Stopwatch.StartNew();
    private readonly Stopwatch m_OptionTimer = Stopwatch.StartNew();

    public async Task ProcessStockQuotes(List<Quote> quotes)
    {
        if (m_StockTimer.Elapsed.TotalSeconds < Configuration.Security.Global.HistoryTimeFrameInMinutes * 60)
            return;

        m_StockTimer.Restart();

        await m_QuoteRepository.CreateQuotes(quotes);
    }

    public async Task ProcessForexQuotes(List<Quote> quotes)
    {
        if (m_ForexTimer.Elapsed.TotalSeconds < Configuration.Security.Global.HistoryTimeFrameInMinutes * 60)
            return;

        m_ForexTimer.Restart();

        await m_QuoteRepository.CreateQuotes(quotes);
    }

    public async Task ProcessOptionQuotes(List<Quote> quotes)
    {
        if (m_OptionTimer.Elapsed.TotalSeconds < Configuration.Security.Global.HistoryTimeFrameInMinutes * 60)
            return;

        m_OptionTimer.Restart();

        await m_QuoteRepository.CreateQuotes(quotes);
    }
}

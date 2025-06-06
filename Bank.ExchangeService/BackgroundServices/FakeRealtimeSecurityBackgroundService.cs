using System.Collections.Concurrent;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Requests;
using Bank.ExchangeService.Database.Processors;
using Bank.ExchangeService.Models;
using Bank.ExchangeService.Repositories;

namespace Bank.ExchangeService.BackgroundServices;

public class FakeRealtimeSecurityBackgroundService(IEnumerable<IRealtimeProcessor> realtimeProcessors, ISecurityRepository securityRepository)
{
    private readonly IEnumerable<IRealtimeProcessor> m_RealtimeProcessors = realtimeProcessors;
    private readonly ISecurityRepository             m_SecurityRepository = securityRepository;

    public ConcurrentQueue<Quote> QuoteQueue { get; } = new();

    public Task OnApplicationStarted(CancellationToken cancellationToken) => Task.CompletedTask;

    private bool m_ProcessingQuotes = false;

    public async Task Add(FakeQuoteRequest request)
    {
        var security = await m_SecurityRepository.FindById(request.SecurityId, new QuoteFilterIntervalQuery { Interval = QuoteIntervalType.Day });
        
        QuoteQueue.Enqueue(new Quote
                           {
                               Id            = Guid.NewGuid(),
                               SecurityId    = request.SecurityId,
                               Security      = security,
                               AskPrice      = request.AskPrice,
                               BidPrice      = request.BidPrice,
                               AskSize       = request.AskSize,
                               BidSize       = request.BidSize,
                               HighPrice     = 0,
                               LowPrice      = 0,
                               ClosePrice    = 0,
                               OpeningPrice  = 0,
                               Volume        = request.Volume,
                               ContractCount = request.ContractCount,
                               CreatedAt     = request.CreatedAt,
                               ModifiedAt    = request.ModifiedAt,
                           });
    }
    
    public async Task ExecuteQuotesSeeder(object? @object = null)
    {
        if (m_ProcessingQuotes || QuoteQueue.IsEmpty)
            return;

        m_ProcessingQuotes = true;

        var quotes = new List<Quote>();

        while (QuoteQueue.TryDequeue(out var quote))
            quotes.Add(quote);

        List<IEnumerable<Task>> tasks=
        [
            m_RealtimeProcessors.Select(realtimeProcessor =>
                                        realtimeProcessor.ProcessStockQuotes(quotes
                                                                             .Where(quote => quote.Security!.SecurityType == SecurityType.Stock)
                                                                             .ToList())),
            m_RealtimeProcessors.Select(realtimeProcessor =>
                                        realtimeProcessor.ProcessForexQuotes(quotes.Where(quote => quote.Security!.SecurityType ==
                                                                                                   SecurityType.ForexPair)
                                                                                   .ToList())),
            m_RealtimeProcessors.Select(realtimeProcessor =>
                                        realtimeProcessor.ProcessOptionQuotes(quotes.Where(quote => quote.Security!.SecurityType ==
                                                                                                    SecurityType.Option)
                                                                                    .ToList()))
        ];

        await Task.WhenAll(tasks.SelectMany(taskList => taskList).ToList());


        m_ProcessingQuotes = false;
    }

    public Task OnApplicationStopped(CancellationToken cancellationToken) => Task.CompletedTask;
}

using Bank.ExchangeService.Models;
using Bank.ExchangeService.Repositories;

namespace Bank.ExchangeService.Database.Processors;

public class InMemoryRealtimeProcessor(IRedisRepository redisRepository) : IRealtimeProcessor
{
    private readonly IRedisRepository m_RedisRepository = redisRepository;

    public async Task ProcessStockQuotes(List<Quote> quotes)
    {
        await m_RedisRepository.AddAllStockQuotes(quotes);
    }

    public async Task ProcessForexQuotes(List<Quote> quotes)
    {
        await m_RedisRepository.AddAllForexPairQuotes(quotes);
    }

    public async Task ProcessOptionQuotes(List<Quote> quotes)
    {
        await m_RedisRepository.AddAllOptionQuotes(quotes);
    }
}

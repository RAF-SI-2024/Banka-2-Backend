using Bank.Application.Domain;
using Bank.Application.Extensions;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Models;

using MessagePack;

using StackExchange.Redis;

using Order = Bank.ExchangeService.Models.Order;

namespace Bank.ExchangeService.Repositories;

public interface IRedisRepository
{
    Task<bool> AddOrder(Order order);

    Task<bool> RemoveOrders(List<RedisOrder> orders);

    Task<List<RedisOrder>> FindAllOrders();

    Task<List<RedisOrder>> FindAllStockOrders();

    Task<List<RedisOrder>> FindAllForexOrders();

    Task<List<RedisOrder>> FindAllFutureOrders();

    Task<List<RedisOrder>> FindAllOptionOrders();

    Task<bool> AddAllStockQuotes(List<Quote> quotes);

    Task<bool> AddAllForexPairQuotes(List<Quote> quotes);

    Task<bool> AddAllOptionQuotes(List<Quote> quotes);

    Task<List<RedisQuote>> FindAllStockQuotes(string ticker);

    Task<List<RedisQuote>> FindAllForexPairQuotes(string ticker);

    Task<List<RedisQuote>> FindAllOptionQuotes(string ticker);

    Task<RedisQuote?> FindLatestStockQuote(string ticker);

    Task<RedisQuote?> FindLatestForexPairQuote(string ticker);

    Task<RedisQuote?> FindLatestOptionQuote(string ticker);

    Task<List<RedisKey>> FindAllKeys(RedisValue pattern);

    Task Clear();
}

public class RedisRepository(IConnectionMultiplexer connectionMultiplexer) : IRedisRepository
{
    private readonly IDatabase m_RedisDatabase = connectionMultiplexer.GetDatabase();

    private readonly IServer m_RedisServer = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()
                                                                                                  .First());

    public async Task<bool> AddOrder(Order order)
    {
        await m_RedisDatabase.StringSetAsync($"order:{(int)order.Security!.SecurityType}:{Convert.ToBase64String(order.Id.ToByteArray())}",
                                             MessagePackSerializer.Serialize(order.ToRedis()));

        return true;
    }

    public async Task<bool> RemoveOrders(List<RedisOrder> orders)
    {
        var result = await m_RedisDatabase.KeyDeleteAsync(orders.Select(order => (RedisKey)order.ToKey())
                                                                .ToArray());

        return result == orders.Count;
    }

    public async Task<List<RedisOrder>> FindAllOrders()
    {
        var keys = await FindAllKeys("order:*:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.Zip(keys, (value, key) => (key, value))
                          .AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(pair => MessagePackSerializer.Deserialize<RedisOrder>(pair.value)
                                                               .MapKey(pair.key))
                          .ToList();
    }

    public async Task<List<RedisOrder>> FindAllStockOrders()
    {
        var keys = await FindAllKeys($"order:{(int)SecurityType.Stock}:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.Zip(keys, (value, key) => (key, value))
                          .AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(pair => MessagePackSerializer.Deserialize<RedisOrder>(pair.value)
                                                               .MapKey(pair.key))
                          .ToList();
    }

    public async Task<List<RedisOrder>> FindAllForexOrders()
    {
        var keys = await FindAllKeys($"order:{(int)SecurityType.ForexPair}:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.Zip(keys, (value, key) => (key, value))
                          .AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(pair => MessagePackSerializer.Deserialize<RedisOrder>(pair.value)
                                                               .MapKey(pair.key))
                          .ToList();
    }

    public async Task<List<RedisOrder>> FindAllOptionOrders()
    {
        var keys = await FindAllKeys($"order:{(int)SecurityType.Option}:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.Zip(keys, (value, key) => (key, value))
                          .AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(pair => MessagePackSerializer.Deserialize<RedisOrder>(pair.value)
                                                               .MapKey(pair.key))
                          .ToList();
    }

    public async Task<List<RedisOrder>> FindAllFutureOrders()
    {
        var keys = await FindAllKeys($"order:{(int)SecurityType.FutureContract}:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.Zip(keys, (value, key) => (key, value))
                          .AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(pair => MessagePackSerializer.Deserialize<RedisOrder>(pair.value)
                                                               .MapKey(pair.key))
                          .ToList();
    }

    public async Task<bool> AddAllStockQuotes(List<Quote> quotes)
    {
        var options           = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
        var time              = DateTime.UtcNow;
        var timeSinceMidnight = time - time.Date;

        await Parallel.ForEachAsync(quotes.Chunk(500), options, async (batch, _) =>
                                                                {
                                                                    var redisBatch = m_RedisDatabase.CreateBatch();
                                                                    var tasks      = new List<Task>();

                                                                    foreach (var quote in batch)
                                                                    {
                                                                        string keyHex = $"s:{quote.Security!.Ticker}:{((int)timeSinceMidnight.TotalSeconds).EncodeToBase64()}";

                                                                        tasks.Add(redisBatch.StringSetAsync(keyHex, MessagePackSerializer.Serialize(quote.ToRedis()),
                                                                                                            TimeSpan.FromDays(1)));
                                                                    }

                                                                    redisBatch.Execute();

                                                                    await Task.WhenAll(tasks);
                                                                });

        return true;
    }

    public async Task<bool> AddAllForexPairQuotes(List<Quote> quotes)
    {
        var options           = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
        var time              = DateTime.UtcNow;
        var timeSinceMidnight = time - time.Date;

        await Parallel.ForEachAsync(quotes.Chunk(500), options, async (batch, _) =>
                                                                {
                                                                    var redisBatch = m_RedisDatabase.CreateBatch();

                                                                    var tasks = new List<Task>();

                                                                    foreach (var quote in batch)
                                                                    {
                                                                        string keyHex = $"f:{quote.Security!.Ticker}:{((int)timeSinceMidnight.TotalSeconds).EncodeToBase64()}";

                                                                        tasks.Add(redisBatch.StringSetAsync(keyHex, MessagePackSerializer.Serialize(quote.ToRedis()),
                                                                                                            TimeSpan.FromDays(1)));
                                                                    }

                                                                    redisBatch.Execute();

                                                                    await Task.WhenAll(tasks);
                                                                });

        return true;
    }

    public async Task<bool> AddAllOptionQuotes(List<Quote> quotes)
    {
        var options           = new ParallelOptions() { MaxDegreeOfParallelism = 4 };
        var time              = DateTime.UtcNow;
        var timeSinceMidnight = time - time.Date;

        await Parallel.ForEachAsync(quotes.Chunk(500), options, async (batch, _) =>
                                                                {
                                                                    var redisBatch = m_RedisDatabase.CreateBatch();
                                                                    var tasks      = new List<Task>();

                                                                    foreach (var quote in batch)
                                                                    {
                                                                        string keyHex = $"o:{quote.Security!.Ticker}:{((int)timeSinceMidnight.TotalSeconds).EncodeToBase64()}";

                                                                        tasks.Add(redisBatch.StringSetAsync(keyHex, MessagePackSerializer.Serialize(quote.ToRedis()),
                                                                                                            TimeSpan.FromDays(1)));
                                                                    }

                                                                    redisBatch.Execute();

                                                                    await Task.WhenAll(tasks);
                                                                });

        return true;
    }

    public async Task<List<RedisQuote>> FindAllStockQuotes(string ticker)
    {
        var keys = await FindAllKeys($"s:{ticker}:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.Zip(keys, (value, key) => (key, value))
                          .AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(redisValue => MessagePackSerializer.Deserialize<RedisQuote>(redisValue.value)
                                                                     .MapKey(redisValue.key))
                          .ToList();
    }

    public async Task<List<RedisQuote>> FindAllForexPairQuotes(string ticker)
    {
        var keys = await FindAllKeys($"f:{ticker}:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.Zip(keys, (value, key) => (key, value))
                          .AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(redisValue => MessagePackSerializer.Deserialize<RedisQuote>(redisValue.value)
                                                                     .MapKey(redisValue.key))
                          .ToList();
    }

    public async Task<List<RedisQuote>> FindAllOptionQuotes(string ticker)
    {
        var keys = await FindAllKeys($"o:{ticker}:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.Zip(keys, (value, key) => (key, value))
                          .AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(redisValue => MessagePackSerializer.Deserialize<RedisQuote>(redisValue.value)
                                                                     .MapKey(redisValue.key))
                          .ToList();
    }

    public async Task<RedisQuote?> FindLatestStockQuote(string ticker)
    {
        var keys = await FindAllKeys($"s:{ticker}:*");

        if (keys.Count is 0)
            return null;

        var latestStockKey = FindLatestKey(keys);

        var redisValue = await m_RedisDatabase.StringGetAsync(latestStockKey);

        return MessagePackSerializer.Deserialize<RedisQuote>(redisValue);
    }

    public async Task<RedisQuote?> FindLatestForexPairQuote(string ticker)
    {
        var keys = await FindAllKeys($"f:{ticker}:*");

        if (keys.Count is 0)
            return null;

        var latestStockKey = FindLatestKey(keys);

        var redisValue = await m_RedisDatabase.StringGetAsync(latestStockKey);

        return MessagePackSerializer.Deserialize<RedisQuote>(redisValue);
    }

    public async Task<RedisQuote?> FindLatestOptionQuote(string ticker)
    {
        var keys = await FindAllKeys($"o:{ticker}:*");

        if (keys.Count is 0)
            return null;

        var latestStockKey = FindLatestKey(keys);

        var redisValue = await m_RedisDatabase.StringGetAsync(latestStockKey);

        return MessagePackSerializer.Deserialize<RedisQuote>(redisValue);
    }

    private RedisKey FindLatestKey(List<RedisKey> keys)
    {
        return keys.Select(key =>
                           {
                               var interval = key.ToString()
                                                 .Split(':')[2]
                                                 .DecodeBase64ToInt();

                               return new { Key = key, Interval = interval };
                           })
                   .OrderByDescending(pair => pair.Interval)
                   .Select(pair => pair.Key)
                   .First();
    }

    // DANGER!!! READ AT YOUR OWN RISK! DO NOT EVER LOOK AT THIS SHIT!
    public async Task<List<RedisKey>> FindAllKeys(RedisValue pattern)
    {
        var            curcor = 0;
        List<RedisKey> keys   = [];

        do
        {
            var result = await m_RedisServer.ExecuteAsync("scan", curcor, "match", pattern, "count", 50_000);

            curcor = (int)result[0];

            keys.AddRange((RedisKey[])result[1]!);
        } while (curcor is not 0);

        return keys;
    }

    public async Task Clear()
    {
        await m_RedisServer.ExecuteAsync("flushdb");
    }
}

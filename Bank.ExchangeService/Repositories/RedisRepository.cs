﻿using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Models;

using MessagePack;

using Microsoft.Extensions.Caching.Distributed;

using StackExchange.Redis;

using Order = Bank.ExchangeService.Models.Order;

namespace Bank.ExchangeService.Repositories;

public interface IRedisRepository
{
    Task<bool> AddOrder(Order order);

    Task<List<RedisOrder>> FindAllOrders();

    Task<List<RedisOrder>> FindAllStockOrders();

    Task<List<RedisOrder>> FindAllForexOrders();

    Task<List<RedisOrder>> FindAllFutureOrders();

    Task<List<RedisOrder>> FindAllOptionOrders();

    Task<List<RedisKey>> FindAllKeys(RedisValue pattern);
}

public class RedisRepository(IConnectionMultiplexer connectionMultiplexer) : IRedisRepository
{
    private readonly IDatabase m_RedisDatabase = connectionMultiplexer.GetDatabase();

    private readonly IServer m_RedisServer = connectionMultiplexer.GetServer(connectionMultiplexer.GetEndPoints()
                                                                                                  .First());

    public async Task<bool> AddOrder(Order order)
    {
        await m_RedisDatabase.StringSetAsync($"order:{(int)order.OrderType}:{Convert.ToBase64String(order.Id.ToByteArray())}", MessagePackSerializer.Serialize(order.ToRedis()));

        return true;
    }

    public async Task<List<RedisOrder>> FindAllOrders()
    {
        var keys = await FindAllKeys("order:*:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(redisValue => MessagePackSerializer.Deserialize<RedisOrder>(redisValue))
                          .ToList();
    }

    public async Task<List<RedisOrder>> FindAllStockOrders()
    {
        var keys = await FindAllKeys("order:stock:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(redisValue => MessagePackSerializer.Deserialize<RedisOrder>(redisValue))
                          .ToList();
    }

    public async Task<List<RedisOrder>> FindAllForexOrders()
    {
        var keys = await FindAllKeys("order:forex:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(redisValue => MessagePackSerializer.Deserialize<RedisOrder>(redisValue))
                          .ToList();
    }

    public async Task<List<RedisOrder>> FindAllFutureOrders()
    {
        var keys = await FindAllKeys("order:future:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(redisValue => MessagePackSerializer.Deserialize<RedisOrder>(redisValue))
                          .ToList();
    }

    public async Task<List<RedisOrder>> FindAllOptionOrders()
    {
        var keys = await FindAllKeys("order:option:*");

        var redisValues = await m_RedisDatabase.StringGetAsync(keys.ToArray());

        return redisValues.AsParallel()
                          .WithDegreeOfParallelism(4)
                          .Select(redisValue => MessagePackSerializer.Deserialize<RedisOrder>(redisValue))
                          .ToList();
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
}

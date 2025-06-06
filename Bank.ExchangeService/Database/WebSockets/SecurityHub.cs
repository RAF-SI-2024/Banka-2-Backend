using System.Collections.Concurrent;

using Bank.Application.Responses;

using Microsoft.AspNetCore.SignalR;

namespace Bank.ExchangeService.Database.WebSockets;

public interface ISecurityClient
{
    Task Subscribe(string message);

    Task Unsubscribe(string message);

    Task ReceiveSecurityUpdate(QuoteLatestSimpleResponse quote);
}

public sealed class SecurityHub : Hub<ISecurityClient>
{
    private static readonly ConcurrentDictionary<string, HashSet<string>> s_SubscribedSecurities = new();

    public async Task Subscribe(IEnumerable<string> tickers)
    {
        var connectionId = Context.ConnectionId;

        if (!s_SubscribedSecurities.TryGetValue(connectionId, out var subscribedSecurity))
        {
            subscribedSecurity                   = [];
            s_SubscribedSecurities[connectionId] = subscribedSecurity;
        }

        var newTickers = new List<string>();

        foreach (var ticker in tickers)
        {
            if (s_SubscribedSecurities[connectionId]
                .Add(ticker))
                newTickers.Add(ticker);
        }

        if (newTickers.Count == 0)
            return;

        await Task.WhenAll(newTickers.Select(ticker => Groups.AddToGroupAsync(connectionId, ticker)));
        await Clients.Caller.Subscribe($"Security: {string.Join(", ", newTickers)}");
    }

    public async Task Unsubscribe(IEnumerable<string> tickers)
    {
        var connectionId = Context.ConnectionId;

        if (!s_SubscribedSecurities.TryGetValue(connectionId, out var subscribedSecurity))
            return;

        var removedTickers = new List<string>();

        foreach (var ticker in tickers)
        {
            if (subscribedSecurity.Remove(ticker))
                removedTickers.Add(ticker);
        }

        if (removedTickers.Count == 0)
            return;

        await Task.WhenAll(removedTickers.Select(ticker => Groups.RemoveFromGroupAsync(connectionId, ticker)));
        await Clients.Caller.Unsubscribe($"Security: {string.Join(", ", removedTickers)}");
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        s_SubscribedSecurities.TryRemove(connectionId, out _);
        await base.OnDisconnectedAsync(exception);
    }
}

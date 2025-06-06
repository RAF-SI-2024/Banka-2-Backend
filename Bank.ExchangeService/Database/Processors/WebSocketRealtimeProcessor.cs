using Bank.ExchangeService.Database.WebSockets;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Models;

using Microsoft.AspNetCore.SignalR;

namespace Bank.ExchangeService.Database.Processors;

public class WebSocketRealtimeProcessor(IHubContext<SecurityHub, ISecurityClient> securityHub) : IRealtimeProcessor
{
    private readonly IHubContext<SecurityHub, ISecurityClient> m_SecurityHub = securityHub;

    public async Task ProcessStockQuotes(List<Quote> quotes)
    {
        foreach (var quote in quotes)
        {
            var quoteLatestSimpleResponse = quote.ToLatestSimpleResponse();

            await m_SecurityHub.Clients.Group(quoteLatestSimpleResponse.SecurityTicker)
                               .ReceiveSecurityUpdate(quoteLatestSimpleResponse);
        }
    }

    public async Task ProcessForexQuotes(List<Quote> quotes)
    {
        foreach (var quote in quotes)
        {
            var quoteLatestSimpleResponse = quote.ToLatestSimpleResponse();

            await m_SecurityHub.Clients.Group(quoteLatestSimpleResponse.SecurityTicker)
                               .ReceiveSecurityUpdate(quoteLatestSimpleResponse);
        }
    }

    public async Task ProcessOptionQuotes(List<Quote> quotes)
    {
        foreach (var quote in quotes)
        {
            var quoteLatestSimpleResponse = quote.ToLatestSimpleResponse();

            await m_SecurityHub.Clients.Group(quoteLatestSimpleResponse.SecurityTicker)
                               .ReceiveSecurityUpdate(quoteLatestSimpleResponse);
        }
    }
}

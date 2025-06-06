using Bank.Application.Endpoints;
using Bank.Application.Requests;
using Bank.ExchangeService.BackgroundServices;

using Microsoft.AspNetCore.Mvc;

namespace Bank.ExchangeService.Controllers;

[ApiController]
public class QuoteController(FakeRealtimeSecurityBackgroundService fakeRealtimeService) : ControllerBase
{
    private readonly FakeRealtimeSecurityBackgroundService m_FakeRealtimeService = fakeRealtimeService;

    [HttpGet(Endpoints.Quote.ProcessQuotes)]
    public async Task<IActionResult> ProcessQuotes()
    {
        await m_FakeRealtimeService.ExecuteQuotesSeeder();

        return Ok();
    }

    [HttpPost(Endpoints.Quote.Create)]
    public async Task<IActionResult> Create(FakeQuoteRequest quoteRequest)
    {
        await m_FakeRealtimeService.Add(quoteRequest);

        return Ok();
    }
}

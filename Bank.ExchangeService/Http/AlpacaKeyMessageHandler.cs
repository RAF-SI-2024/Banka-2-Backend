using Bank.ExchangeService.Configurations;

namespace Bank.ExchangeService.Http;

public class AlpacaKeyMessageHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var (apiKey, apiSecret) = Configuration.Security.Keys.AlpacaApiKeyAndSecret;

        request.Headers.Add("APCA-API-KEY-ID",     apiKey);
        request.Headers.Add("APCA-API-SECRET-KEY", apiSecret);

        return await base.SendAsync(request, cancellationToken);
    }
}

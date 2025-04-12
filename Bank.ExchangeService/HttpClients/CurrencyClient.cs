using Bank.Application.Responses;

namespace Bank.ExchangeService.HttpClients;

public interface ICurrencyClient
{
    Task<CurrencyResponse?> GetCurrencyById(Guid id);
}

public class CurrencyClient(HttpClient httpClient) : ICurrencyClient
{
    private readonly HttpClient m_httpClient = httpClient;

    public async Task<CurrencyResponse?> GetCurrencyById(Guid id)
    {
        var response = await m_httpClient.GetAsync($"/api/v1/currencies/{id}");

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<CurrencyResponse>();
    }
}

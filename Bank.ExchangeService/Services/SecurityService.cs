using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Responses;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

namespace Bank.ExchangeService.Services;

public interface ISecurityService
{
    Task<Result<SecuritySimpleResponse>> GetOneSimple(Guid id);
}

public class SecurityService(ISecurityRepository securityRepository, IUserServiceHttpClient userServiceHttpClient) : ISecurityService
{
    private readonly ISecurityRepository    m_SecurityRepository    = securityRepository;
    private readonly IUserServiceHttpClient m_UserServiceHttpClient = userServiceHttpClient;

    public async Task<Result<SecuritySimpleResponse>> GetOneSimple(Guid id)
    {
        var security = await m_SecurityRepository.FindByIdSimple(id);

        if (security is null)
            return Result.NotFound<SecuritySimpleResponse>($"No Security found wih Id: {id}");

        var currencyResponse = await m_UserServiceHttpClient.GetOneSimpleCurrency(security.StockExchange!.CurrencyId);

        if (currencyResponse is null)
            throw new Exception($"No Currency with Id: {security.StockExchange!.CurrencyId}");

        CurrencySimpleResponse? baseCurrency  = null;
        CurrencySimpleResponse? quoteCurrency = null;

        if (security.SecurityType == SecurityType.ForexPair)
        {
            var currencyBaseResponseTask  = m_UserServiceHttpClient.GetOneSimpleCurrency(security.BaseCurrencyId);
            var currencyQuoteResponseTask = m_UserServiceHttpClient.GetOneSimpleCurrency(security.QuoteCurrencyId);

            Task.WaitAll(currencyBaseResponseTask, currencyQuoteResponseTask);

            baseCurrency  = currencyBaseResponseTask.Result;
            quoteCurrency = currencyQuoteResponseTask.Result;
        }

        return Result.Ok(security.ToSecuritySimpleResponse(currencyResponse, baseCurrency, quoteCurrency));
    }
}

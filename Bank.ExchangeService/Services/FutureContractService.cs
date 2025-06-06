using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

using Result = Bank.Application.Endpoints.Result;

namespace Bank.ExchangeService.Services;

public interface IFutureContractService
{
    Task<Result<Page<FutureContractSimpleResponse>>> GetAll(QuoteFilterQuery quoteFilterQuery, Pageable pageable);

    Task<Result<FutureContractResponse>> GetOne(Guid id, QuoteFilterIntervalQuery filter);

    Task<Result<FutureContractDailyResponse>> GetOneDaily(Guid id, QuoteFilterIntervalQuery filter);
}

public class FutureContractService(ISecurityRepository securityRepository, IUserServiceHttpClient userServiceHttpClient) : IFutureContractService
{
    private readonly ISecurityRepository    m_SecurityRepository    = securityRepository;
    private readonly IUserServiceHttpClient m_UserServiceHttpClient = userServiceHttpClient;

    public async Task<Result<Page<FutureContractSimpleResponse>>> GetAll(QuoteFilterQuery quoteFilterQuery, Pageable pageable)
    {
        var page = await m_SecurityRepository.FindAll(quoteFilterQuery, SecurityType.FutureContract, pageable, false);

        var responses = page.Items.Select(contract => contract.ToFutureContract()
                                                              .ToSimpleResponse())
                            .ToList();

        return Result.Ok(new Page<FutureContractSimpleResponse>(responses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<FutureContractResponse>> GetOne(Guid id, QuoteFilterIntervalQuery filter)
    {
        var futureContract = await m_SecurityRepository.FindById(id, filter);

        if (futureContract is null)
            return Result.NotFound<FutureContractResponse>($"No FutureContract found wih Id: {id}");

        var currencyResponse = await m_UserServiceHttpClient.GetOneSimpleCurrency(futureContract.StockExchange!.CurrencyId);

        if (currencyResponse is null)
            throw new Exception($"No Currency with Id: {futureContract.StockExchange!.CurrencyId}");

        return Result.Ok(futureContract.ToFutureContract()
                                       .ToResponse(currencyResponse));
    }

    public async Task<Result<FutureContractDailyResponse>> GetOneDaily(Guid id, QuoteFilterIntervalQuery filter)
    {
        var futureContract = await m_SecurityRepository.FindByIdDaily(id, filter);

        if (futureContract is null)
            return Result.NotFound<FutureContractDailyResponse>($"No FutureContract found wih Id: {id}");

        var currencyResponse = await m_UserServiceHttpClient.GetOneSimpleCurrency(futureContract.StockExchange!.CurrencyId);

        if (currencyResponse is null)
            throw new Exception($"No Currency with Id: {futureContract.StockExchange!.CurrencyId}");

        return Result.Ok(futureContract.ToFutureContract()
                                       .ToDailyResponse(currencyResponse));
    }
}

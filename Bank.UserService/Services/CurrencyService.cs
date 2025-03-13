using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ICurrencyService
{
    Task<Result<List<CurrencyResponse>>> FindAll(CurrencyFilterQuery currencyFilterQuery);

    Task<Result<CurrencyResponse>> FindById(Guid id);
}

public class CurrencyService(ICurrencyRepository currencyRepository) : ICurrencyService
{
    private readonly ICurrencyRepository m_CurrencyRepository = currencyRepository;

    public async Task<Result<List<CurrencyResponse>>> FindAll(CurrencyFilterQuery currencyFilterQuery)
    {
        var currencies = await m_CurrencyRepository.FindAll(currencyFilterQuery);

        var currencyResponses = currencies.Select(currency => currency.ToResponse())
                                          .ToList();

        return Result.Ok(currencyResponses);
    }

    public async Task<Result<CurrencyResponse>> FindById(Guid id)
    {
        var currency = await m_CurrencyRepository.FindById(id);

        if (currency == null)
            return Result.NotFound<CurrencyResponse>($"No Currency found with Id: {id}");

        return Result.Ok(currency.ToResponse());
    }
}

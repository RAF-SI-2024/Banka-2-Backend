using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.UserService.Mappers;
using Bank.UserService.Repositories;

namespace Bank.UserService.Services;

public interface ICurrencyService
{
    Task<Result<Page<CurrencyResponse>>> FindAll(CurrencyFilterQuery currencyFilterQuery, Pageable pageable);

    Task<Result<CurrencyResponse>> FindById(Guid id);
}

public class CurrencyService(ICurrencyRepository currencyRepository) : ICurrencyService
{
    private readonly ICurrencyRepository m_CurrencyRepository = currencyRepository;

    public async Task<Result<Page<CurrencyResponse>>> FindAll(CurrencyFilterQuery currencyFilterQuery, Pageable pageable)
    {
        var page = await m_CurrencyRepository.FindAll(currencyFilterQuery, pageable);

        var currencyResponses = page.Items.Select(currency => currency.ToResponse())
                                    .ToList();

        return Result.Ok(new Page<CurrencyResponse>(currencyResponses, page.PageNumber, page.PageSize, page.TotalElements));
    }

    public async Task<Result<CurrencyResponse>> FindById(Guid id)
    {
        var currency = await m_CurrencyRepository.FindById(id);

        if (currency == null)
            return Result.NotFound<CurrencyResponse>($"No Currency found with Id: {id}");

        return Result.Ok(currency.ToResponse());
    }
}

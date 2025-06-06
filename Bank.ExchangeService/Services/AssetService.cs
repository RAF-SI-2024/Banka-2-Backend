using Bank.Application.Domain;
using Bank.Application.Endpoints;
using Bank.Application.Queries;
using Bank.Application.Responses;
using Bank.ExchangeService.Mappers;
using Bank.ExchangeService.Repositories;
using Bank.Http.Clients.User;

namespace Bank.ExchangeService.Services;

public interface IAssetService
{
    Task<Result<Page<AssetResponse>>> GetAll(AssetFilterQuery filter, Pageable pageable);

    Task<Result<Page<AssetResponse>>> GetAllByActuaryId(Guid actuaryId, Pageable pageable);

    Task<Result<AssetResponse>> GetOne(Guid id);
}

public class AssetService(IAssetRepository assetRepository, IUserServiceHttpClient userServiceHttpClient) : IAssetService
{
    private readonly IAssetRepository       m_AssetRepository       = assetRepository;
    private readonly IUserServiceHttpClient m_UserServiceHttpClient = userServiceHttpClient;

    public async Task<Result<Page<AssetResponse>>> GetAll(AssetFilterQuery filter, Pageable pageable)
    {
        var assets = await m_AssetRepository.FindAll(filter, pageable);

        var userQuery = new UserFilterQuery
                        {
                            Ids = assets.Items.Select(asset => asset.ActuaryId)
                                        .Distinct()
                                        .ToList()
                        };

        var userPageable = Pageable.Create(1, userQuery.Ids.Count);

        var userPage = await m_UserServiceHttpClient.GetAllUsers(userQuery, userPageable);

        var userDictionary = userPage.Items.ToDictionary(user => user.Id, user => user);

        var result = assets.Items.Select(asset => asset.ToResponse(userDictionary[asset.ActuaryId]))
                           .ToList();

        return Result.Ok(new Page<AssetResponse>(result, assets.PageNumber, assets.PageSize, assets.TotalElements));
    }

    public async Task<Result<Page<AssetResponse>>> GetAllByActuaryId(Guid actuaryId, Pageable pageable)
    {
        var assets = await m_AssetRepository.FindAllByActuaryId(actuaryId, pageable);
        
        var userQuery = new UserFilterQuery
                        {
                            Ids = assets.Items.Select(asset => asset.ActuaryId)
                                        .Distinct()
                                        .ToList()
                        };

        var userPageable = Pageable.Create(1, userQuery.Ids.Count);

        var userPage = await m_UserServiceHttpClient.GetAllUsers(userQuery, userPageable);

        var userDictionary = userPage.Items.ToDictionary(user => user.Id, user => user);

        var result = assets.Items.Select(asset => asset.ToResponse(userDictionary[asset.ActuaryId]))
                           .ToList();

        return Result.Ok(new Page<AssetResponse>(result, assets.PageNumber, assets.PageSize, assets.TotalElements));
    }

    public async Task<Result<AssetResponse>> GetOne(Guid id)
    {
        var asset = await m_AssetRepository.FindById(id);

        if (asset == null)
            return Result.NotFound<AssetResponse>($"No Asset found with Id: {id}");

        var actuary = await m_UserServiceHttpClient.GetOneUser(asset.ActuaryId);

        return Result.Ok(asset.ToResponse(actuary!));
    }
}

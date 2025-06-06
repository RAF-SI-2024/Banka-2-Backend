using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Database.Core;
using Bank.ExchangeService.Models;
using Bank.Permissions.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

using DatabaseContext = Bank.ExchangeService.Database.DatabaseContext;

namespace Bank.ExchangeService.Repositories;

public interface IAssetRepository
{
    Task<Page<Asset>> FindAll(AssetFilterQuery filter, Pageable pageable);

    Task<Page<Asset>> FindAllByActuaryId(Guid actuaryId, Pageable pageable);

    Task<Asset?> FindById(Guid id);

    public Task<List<Asset>> FindAllAssetsBySecurityAndActuary(List<(Guid SecurityId, Guid ActuaryId)> securityActuaryList);

    Task<bool> Add(Asset asset);

    Task<bool> HasAsset(Guid securityId, Guid actuaryId, int quantity);

    Task<bool> Remove(Asset asset);
}

public class AssetRepository(IDatabaseContextFactory<DatabaseContext> contextFactory, IAuthorizationServiceFactory authorizationServiceFactory) : IAssetRepository
{
    private readonly IDatabaseContextFactory<DatabaseContext> m_ContextFactory              = contextFactory;
    private readonly IAuthorizationServiceFactory             m_AuthorizationServiceFactory = authorizationServiceFactory;

    public async Task<Page<Asset>> FindAll(AssetFilterQuery filter, Pageable pageable)
    {
        await using var context              = await m_ContextFactory.CreateContext;
        var             authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var assetQuery = context.Assets.IncludeAll()
                                .AsQueryable();

        if (authorizationService.Permissions == Permission.Client || authorizationService.Permissions == Permission.Employee)
            assetQuery = assetQuery.Where(asset => asset.ActuaryId == authorizationService.UserId);

        assetQuery = assetQuery.OrderByDescending(asset => asset.ModifiedAt);

        var assets = await assetQuery.Skip((pageable.Page - 1) * pageable.Size)
                                     .Take(pageable.Size)
                                     .ToListAsync();

        var totalElements = await assetQuery.CountAsync();

        return new Page<Asset>(assets, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Page<Asset>> FindAllByActuaryId(Guid actuaryId, Pageable pageable)
    {
        await using var context              = await m_ContextFactory.CreateContext;
        var             authorizationService = m_AuthorizationServiceFactory.AuthorizationService;

        var assetQuery = context.Assets.IncludeAll()
                                .Where(asset => asset.ActuaryId == actuaryId)
                                .AsQueryable();

        if (authorizationService.Permissions == Permission.Client || authorizationService.Permissions == Permission.Employee)
            assetQuery = assetQuery.Where(asset => asset.ActuaryId == authorizationService.UserId);

        assetQuery = assetQuery.OrderByDescending(asset => asset.ModifiedAt);

        var assets = await assetQuery.Skip((pageable.Page - 1) * pageable.Size)
                                     .Take(pageable.Size)
                                     .ToListAsync();

        var totalElements = await assetQuery.CountAsync();
    
        return new Page<Asset>(assets, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<Asset?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Assets.IncludeAll()
                            .FirstOrDefaultAsync(asset => asset.Id == id);
    }

    public async Task<List<Asset>> FindAllAssetsBySecurityAndActuary(List<(Guid SecurityId, Guid ActuaryId)> securityActuaryList)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var assets = await context.Assets.IncludeAll()
                                  .ToListAsync();

        return assets.Where(asset => securityActuaryList.Contains((asset.SecurityId, asset.ActuaryId)))
                     .ToList();
    }

    public async Task<bool> Add(Asset asset)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var result = await context.Assets.Where(dbAsset => dbAsset.ActuaryId == asset.ActuaryId && dbAsset.SecurityId == asset.SecurityId)
                                  .ExecuteUpdateAsync(setters => setters.SetProperty(dbAsset => dbAsset.ModifiedAt, DateTime.UtcNow)
                                                                        .SetProperty(dbAsset => dbAsset.Quantity, dbAsset => dbAsset.Quantity + asset.Quantity)
                                                                        .SetProperty(dbAsset => dbAsset.AveragePrice,
                                                                                     dbAsset => dbAsset.AveragePrice + asset.Quantity *
                                                                                                (asset.AveragePrice - dbAsset.AveragePrice) / (asset.Quantity + dbAsset.Quantity)));

        if (result == 1)
            return true;

        await context.Assets.AddAsync(asset);

        await context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> HasAsset(Guid securityId, Guid actuaryId, int quantity)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var result = await context.Assets.Where(dbAsset => dbAsset.ActuaryId == actuaryId && dbAsset.SecurityId == securityId && dbAsset.Quantity >= quantity)
                                  .AnyAsync();

        return result;
    }

    public async Task<bool> Remove(Asset asset)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var result = await context.Assets.Where(dbAsset => dbAsset.ActuaryId == asset.ActuaryId && dbAsset.SecurityId == asset.SecurityId)
                                  .ExecuteUpdateAsync(setters => setters.SetProperty(dbAsset => dbAsset.ModifiedAt, DateTime.UtcNow)
                                                                        .SetProperty(dbAsset => dbAsset.Quantity, dbAsset => dbAsset.Quantity - asset.Quantity));

        return result == 1;
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<Asset, object?> IncludeAll(this DbSet<Asset> set)
    {
        return set.Include(asset => asset.Security);
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, Asset?> value,
                                                                                 Expression<Func<TEntity, Asset?>>          navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Asset.Security)))
            query = query.Include(navigationExpression)
                         .ThenInclude(asset => asset!.Security);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<Asset>> value,
                                                                                 Expression<Func<TEntity, List<Asset>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(Asset.Security)))
            query = query.Include(navigationExpression)
                         .ThenInclude(asset => asset.Security);

        return query;
    }
}

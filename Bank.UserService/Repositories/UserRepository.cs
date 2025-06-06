using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Utilities;
using Bank.Database.Core;
using Bank.UserService.Database;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

using EFCore.BulkExtensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IUserRepository
{
    Task<Page<User>> FindAll(UserFilterQuery userFilterQuery, Pageable pageable);

    Task<User?> FindById(Guid id);

    Task<User?> FindByEmail(string email);

    Task<User> Add(User user);

    Task<bool> AddRange(IEnumerable<User> users);

    Task<User> Update(User user);

    Task<bool> SetPassword(Guid id, string password);

    Task<bool> Exists(Guid id);

    Task<bool> IsEmpty();
}

public class UserRepository(IDatabaseContextFactory<ApplicationContext> contextFactory) : IUserRepository
{
    private readonly IDatabaseContextFactory<ApplicationContext> m_ContextFactory = contextFactory;

    public async Task<Page<User>> FindAll(UserFilterQuery userFilterQuery, Pageable pageable)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var userQuery = context.Users.IncludeAll()
                               .AsQueryable();

        userQuery = userQuery.Where(user => user.BankId == Seeder.Bank.Bank02.Id);

        if (!string.IsNullOrEmpty(userFilterQuery.FirstName))
            userQuery = userQuery.Where(user => EF.Functions.ILike(user.FirstName, $"%{userFilterQuery.FirstName}%"));

        if (!string.IsNullOrEmpty(userFilterQuery.LastName))
            userQuery = userQuery.Where(user => EF.Functions.ILike(user.LastName, $"%{userFilterQuery.LastName}%"));

        if (!string.IsNullOrEmpty(userFilterQuery.Email))
            userQuery = userQuery.Where(user => EF.Functions.ILike(user.Email, $"%{userFilterQuery.Email}%"));

        if (userFilterQuery.Ids.Count > 0)
            userQuery = userQuery.Where(user => userFilterQuery.Ids.Contains(user.Id));

        if (userFilterQuery.Role != Role.Invalid)
            userQuery = userQuery.Where(user => user.Role == userFilterQuery.Role);
        
        if (userFilterQuery.Permissions != (int)Permission.Invalid)
            userQuery = userQuery.Where(user => (user.Permissions & userFilterQuery.Permissions) == userFilterQuery.Permissions);
        
        int totalElements = await userQuery.CountAsync();

        var users = await userQuery.Skip((pageable.Page - 1) * pageable.Size)
                                   .Take(pageable.Size)
                                   .ToListAsync();

        return new Page<User>(users, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<User?> FindById(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Users.IncludeAll()
                            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> FindByEmail(string email)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Users.IncludeAll()
                            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> Add(User user)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var addedUser = await context.Users.AddAsync(user);

        await context.SaveChangesAsync();

        return addedUser.Entity;
    }

    public async Task<bool> AddRange(IEnumerable<User> users)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.BulkInsertAsync(users, config => config.BatchSize = 2000);

        return true;
    }

    public async Task<User> Update(User user)
    {
        await using var context = await m_ContextFactory.CreateContext;

        await context.Users.Where(dbUser => dbUser.Id == user.Id)
                     .ExecuteUpdateAsync(setters => setters.SetProperty(dbUser => dbUser.FirstName, user.FirstName)
                                                           .SetProperty(dbUser => dbUser.LastName,    user.LastName)
                                                           .SetProperty(dbUser => dbUser.Username,    user.Username)
                                                           .SetProperty(dbUser => dbUser.PhoneNumber, user.PhoneNumber)
                                                           .SetProperty(dbUser => dbUser.Address,     user.Address)
                                                           .SetProperty(dbUser => dbUser.Role,        user.Role)
                                                           .SetProperty(dbUser => dbUser.Department,  user.Department)
                                                           .SetProperty(dbUser => dbUser.Employed,    user.Employed)
                                                           .SetProperty(dbUser => dbUser.Activated,   user.Activated)
                                                           .SetProperty(dbUser => dbUser.ModifiedAt,  user.ModifiedAt)
                                                           .SetProperty(dbUser => dbUser.Permissions, user.Permissions));

        return user;
    }

    public async Task<bool> SetPassword(Guid id, string password)
    {
        await using var context = await m_ContextFactory.CreateContext;

        var userEntity = await FindById(id);

        if (userEntity is null)
            return false;

        password = HashingUtilities.HashPassword(password, userEntity.Salt);

        var result = await context.Users.Where(user => user.Id == id)
                                  .ExecuteUpdateAsync(setters => setters.SetProperty(dbUser => dbUser.Password, password)
                                                                        .SetProperty(dbUser => dbUser.Activated, true));

        return result == 1;
    }

    public async Task<bool> Exists(Guid id)
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Users.AnyAsync(users => users.Id == id);
    }

    public async Task<bool> IsEmpty()
    {
        await using var context = await m_ContextFactory.CreateContext;

        return await context.Users.AnyAsync() is not true;
    }
}

public static partial class RepositoryExtensions
{
    public static IIncludableQueryable<User, object?> IncludeAll(this DbSet<User> set)
    {
        return set.Include(user => user.Bank)
                  .ThenIncludeAll(user => user.Bank)
                  .Include(user => user.Accounts)
                  .ThenIncludeAll(user => user.Accounts, nameof(Account.Employee), nameof(Account.Client));
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, User?> value,
                                                                                 Expression<Func<TEntity, User?>>          navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(User.Bank)))
            query = query.Include(navigationExpression)
                         .ThenInclude(user => user!.Bank);

        if (!excludeProperties.Contains(nameof(User.Accounts)))
            query = query.Include(navigationExpression)
                         .ThenInclude(user => user!.Accounts);

        return query;
    }

    public static IIncludableQueryable<TEntity, object?> ThenIncludeAll<TEntity>(this IIncludableQueryable<TEntity, List<User>> value,
                                                                                 Expression<Func<TEntity, List<User>>> navigationExpression, params string[] excludeProperties)
    where TEntity : class
    {
        IIncludableQueryable<TEntity, object?> query = value;

        if (!excludeProperties.Contains(nameof(User.Bank)))
            query = query.Include(navigationExpression)
                         .ThenInclude(user => user.Bank);

        if (!excludeProperties.Contains(nameof(User.Accounts)))
            query = query.Include(navigationExpression)
                         .ThenInclude(user => user.Accounts);

        return query;
    }
}

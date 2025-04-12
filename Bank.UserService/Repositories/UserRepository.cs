using System.Linq.Expressions;

using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Utilities;
using Bank.UserService.Database;
using Bank.UserService.Database.Seeders;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace Bank.UserService.Repositories;

public interface IUserRepository
{
    Task<Page<User>> FindAll(UserFilterQuery userFilterQuery, Pageable pageable);

    Task<User?> FindById(Guid id);

    Task<User?> FindByEmail(string email);

    Task<User> Add(User user);

    Task<User> Update(User user);

    Task<User> SetPassword(Guid id, string password);
}

public class UserRepository(ApplicationContext context) : IUserRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<User>> FindAll(UserFilterQuery userFilterQuery, Pageable pageable)
    {
        var userQuery = m_Context.Users.IncludeAll()
                                 .AsQueryable();

        userQuery = userQuery.Where(user => user.BankId == Seeder.Bank.Bank02.Id);

        if (!string.IsNullOrEmpty(userFilterQuery.FirstName))
            userQuery = userQuery.Where(user => EF.Functions.ILike(user.FirstName, $"%{userFilterQuery.FirstName}%"));

        if (!string.IsNullOrEmpty(userFilterQuery.LastName))
            userQuery = userQuery.Where(user => EF.Functions.ILike(user.LastName, $"%{userFilterQuery.LastName}%"));

        if (!string.IsNullOrEmpty(userFilterQuery.Email))
            userQuery = userQuery.Where(user => EF.Functions.ILike(user.Email, $"%{userFilterQuery.Email}%"));

        if (userFilterQuery.Role != Role.Invalid)
            userQuery = userQuery.Where(user => user.Role == userFilterQuery.Role);

        int totalElements = await userQuery.CountAsync();

        var users = await userQuery.Skip((pageable.Page - 1) * pageable.Size)
                                   .Take(pageable.Size)
                                   .ToListAsync();

        return new Page<User>(users, pageable.Page, pageable.Size, totalElements);
    }

    public async Task<User?> FindById(Guid id)
    {
        return await m_Context.Users.IncludeAll()
                              .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> FindByEmail(string email)
    {
        return await m_Context.Users.IncludeAll()
                              .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> Add(User user)
    {
        var addedUser = await m_Context.Users.AddAsync(user);

        await m_Context.SaveChangesAsync();

        return addedUser.Entity;
    }

    public async Task<User> Update(User user)
    {
        await m_Context.Users.Where(dbUser => dbUser.Id == user.Id)
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
                                                             .SetProperty(dbUser => dbUser.Permissions,  user.Permissions));

        return user;
    }

    public async Task<User> SetPassword(Guid id, string password)
    {
        var user = await FindById(id);

        if (user == null)
            throw new Exception("User not found.");

        user.Password  = HashingUtilities.HashPassword(password, user.Salt);
        user.Activated = true;

        await m_Context.SaveChangesAsync();

        return user;
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
                                                                                 Expression<Func<TEntity, List<User>>>          navigationExpression, params string[] excludeProperties)
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

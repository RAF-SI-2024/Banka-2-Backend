using Bank.Application.Domain;
using Bank.Application.Queries;
using Bank.Application.Utilities;
using Bank.UserService.Database;
using Bank.UserService.Models;

using Microsoft.EntityFrameworkCore;

namespace Bank.UserService.Repositories;

public interface IUserRepository
{
    Task<Page<User>> FindAll(UserFilterQuery userFilterQuery, Pageable pageable);

    Task<User?> FindById(Guid id);

    Task<User?> FindByEmail(string email);

    Task<User> Add(User user);

    Task<User> Update(User oldUser, User user);

    Task<User> SetPassword(Guid id, string password);
}

public class UserRepository(ApplicationContext context) : IUserRepository
{
    private readonly ApplicationContext m_Context = context;

    public async Task<Page<User>> FindAll(UserFilterQuery userFilterQuery, Pageable pageable)
    {
        var userQuery = m_Context.Users.AsQueryable();

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
        return await m_Context.Users.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<User?> FindByEmail(string email)
    {
        return await m_Context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User> Add(User user)
    {
        var addedUser = await m_Context.Users.AddAsync(user);

        await m_Context.SaveChangesAsync();

        return addedUser.Entity;
    }

    public async Task<User> Update(User oldUser, User user)
    {
        m_Context.Users.Entry(oldUser)
                 .State = EntityState.Detached;

        var updatedUser = m_Context.Users.Update(user);

        await m_Context.SaveChangesAsync();

        return updatedUser.Entity;
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

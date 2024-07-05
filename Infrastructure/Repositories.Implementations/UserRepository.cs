using Domain;
using Exceptions.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class UserRepository(DbContext dbContext) : IUserRepository
{
    public async Task<User> GetByLogin(User user)
    {
        var result = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Login == user.Login);
        if (result != null)
            return result;
        
        throw new DomainException
        {
            Title = "User not found",
            Message = $"User with this login not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
    
    public async Task<Guid> AddAsync(User user)
    {
        try
        {
            user.Id = Guid.NewGuid();
            await dbContext.Set<User>().AddAsync(user);
            await dbContext.SaveChangesAsync();
        
            return user.Id;
        }
        catch
        {
            throw new DomainException
            {
                Title = "Create user failed",
                Message = "User with this login already exists",
                StatusCode = StatusCodes.Status409Conflict
            };
        }
    }
    
    public async Task<User> DeleteAsync(Guid id)
    {
        var user = await dbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
        if (user != null)
        {
            dbContext.Set<User>().Remove(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        throw new DomainException
        {
            Title = "Delete user failed",
            Message = $"User with id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
}
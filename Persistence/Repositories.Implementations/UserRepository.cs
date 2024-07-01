using Domain;
using Exceptions.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Persistence.Repositories.Implementations;

public class UserRepository(DbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<User> GetByLogin(User user)
    {
        var result = await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Login == user.Login);
        if (result != null)
            return result;
        
        throw new DomainException
        {
            Title = "User not found",
            Message = $"User with this login not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
}
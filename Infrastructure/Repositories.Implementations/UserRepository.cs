using Domain;
using Exceptions.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class UserRepository(DbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public async Task<User> GetByLogin(User user)
    {
        var result = await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Login == user.Login);
        if (result != null)
            return result;
        else
            throw new DomainException
            {
                Title = "User not found",
                Message = $"User with this login: {user.Login} not found",
                StatusCode = StatusCodes.Status404NotFound
            };
    }
}
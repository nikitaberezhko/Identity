using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class UserRepository(DbContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public Task<User?> GetByLogin(User user)
    {
        return DbContext.Set<User>().FirstOrDefaultAsync(x => x.Login == user.Login);
    }

    public override async Task<User?> DeleteAsync(Guid id)
    {
        var user = await DbContext.Set<User>().FirstOrDefaultAsync(x => x.Id == id);
        if (user != null)
        {
            user.IsDeleted = true;
            await DbContext.SaveChangesAsync();
            return user;
        }

        return null;
    }
}
using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbContext dbContext) : base(dbContext) { }
    
    public Task<User> GetByLogin(User user)
    {
        return DbContext.Set<User>().FirstOrDefaultAsync(x => x.Login == user.Login);
    }
}
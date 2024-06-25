using Domain;

namespace Services.Repositories.Abstractions;

public interface IUserRepository : IRepository<User>
{
    public Task<User> GetByLogin(User user);
    
    public Task<User?> DeleteAsync(Guid id);
}
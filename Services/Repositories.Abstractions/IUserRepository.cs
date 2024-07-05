using Domain;

namespace Services.Repositories.Abstractions;

public interface IUserRepository
{
    public Task<User> GetByLogin(User user);

    public Task<Guid> AddAsync(User entity);

    public Task<User> DeleteAsync(Guid id);
}
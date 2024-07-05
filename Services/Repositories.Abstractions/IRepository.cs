using Domain;

namespace Services.Repositories.Abstractions;

public interface IRepository<T> where T : BaseEntity
{
    public Task<Guid> AddAsync(T entity);
    
    public Task<T> DeleteAsync(Guid id);
}
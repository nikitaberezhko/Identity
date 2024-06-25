using Domain;

namespace Services.Repositories.Abstractions;

public interface IRepository<T> where T : BaseEntity
{
    public Task<List<T>> GetAllAsync();

    public Task<T?> GetAsync(Guid id);
    
    public Task<Guid> AddAsync(T entity);

    public Task<bool> UpdateAsync(T entity);
    
    public Task<T> DeleteAsync(Guid id);
}
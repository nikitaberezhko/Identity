using Domain;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Infrastructure.Repositories.Implementations;

public abstract class Repository<T> : IRepository<T> 
    where T : BaseEntity
{
    protected readonly DbContext DbContext;
    
    public Repository(DbContext dbContext)
    {
        DbContext = dbContext;
    }
    
    public virtual async Task<List<T>> GetAllAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }
    
    public virtual async Task<T?> GetAsync(Guid id)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public virtual async Task<bool> AddAsync(T entity)
    {
        entity.Id = Guid.NewGuid();
        await DbContext.Set<T>().AddAsync(entity);
        await DbContext.SaveChangesAsync();
        
        return true;
    }
    
    public virtual async Task<bool> UpdateAsync(T entity)
    {
        var obj = await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == entity.Id);
        if (obj != null)
        {
            obj = entity;
            await DbContext.SaveChangesAsync();
            return true;
        }
        
        return false;
    }
    
    public virtual async Task<bool> DeleteAsync(Guid id)
    {
        var entity = await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
            return true;
        }

        return false;
    }
}
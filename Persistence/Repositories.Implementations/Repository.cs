using Domain;
using Exceptions.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Abstractions;

namespace Persistence.Repositories.Implementations;

public abstract class Repository<T>(DbContext dbContext) : IRepository<T>
    where T : BaseEntity
{
    protected readonly DbContext DbContext = dbContext;

    public virtual async Task<List<T>> GetAllAsync()
    {
        return await DbContext.Set<T>().ToListAsync();
    }
    
    public virtual async Task<T?> GetAsync(Guid id)
    {
        return await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
    }
    
    public virtual async Task<Guid> AddAsync(T entity)
    {
        try
        {
            entity.Id = Guid.NewGuid();
            await DbContext.Set<T>().AddAsync(entity);
            await DbContext.SaveChangesAsync();
        
            return entity.Id;
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
    
    public virtual async Task<T> DeleteAsync(Guid id)
    {
        var entity = await DbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        throw new DomainException
        {
            Title = "Delete user failed",
            Message = $"User with id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
}
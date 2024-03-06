using CommonCrm.Data.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CommonCrm.Data.Repositories.Concrete;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class
{
    protected readonly DbContext context;
    public Repository(DbContext ctx)
    {
        context = ctx;
    }
    public async Task Create(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
    }

    public async Task Delete(TEntity entity)
    {
         context.Set<TEntity>().Remove(entity);
    }

    public async Task<List<TEntity>> GetAll()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetById(int id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public async virtual Task Update(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Modified;
    } 
}
using ForceGet.Domain.Interfaces;
using ForceGet.Infrastructure.Data;

using Microsoft.EntityFrameworkCore;

using System.Linq.Expressions;

namespace ForceGet.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.Where(predicate).ToListAsync();
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet.FirstOrDefaultAsync(predicate);
    }

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await CommitAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
        await CommitAsync();
        return entities;
    }

    public async Task<T> RemoveAsync(T entity)
    {
        _dbSet.Remove(entity);
        await CommitAsync();
        return entity;
    }

    public async Task<IEnumerable<T>> RemoveRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
        await CommitAsync();
        return entities;
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await CommitAsync();
    }

    public async Task UpdateRangeAsync(IEnumerable<T> entities)
    {
        _dbSet.UpdateRange(entities);
        await CommitAsync();
    }
    private async Task<int> CommitAsync()
    {
        using (var transaction = _context.Database.BeginTransaction())
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}

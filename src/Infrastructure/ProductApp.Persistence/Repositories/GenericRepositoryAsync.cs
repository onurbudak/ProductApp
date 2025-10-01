using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Common;
using ProductApp.Domain.Entities;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories;

public class GenericRepositoryAsync<TEntity, TId> : IGenericRepositoryAsync<TEntity, TId> where TEntity : BaseEntity<TId> where TId : notnull
{
    private readonly ApplicationDbContext _dbContext;

    public GenericRepositoryAsync(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAllAsync() => await _dbContext.Set<TEntity>().Where(e => !e.IsDeleted).ToListAsync();

    public async Task<TEntity?> GetByIdAsync(TId id) => await _dbContext.Set<TEntity>().Where(e => !e.IsDeleted).SingleOrDefaultAsync(e => e.Id.Equals(id));

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreateDate = DateTime.Now;
        await _dbContext.AddAsync(entity);
        await _dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity?> UpdateAsync(TEntity entity)
    {
        TEntity? existingEntity = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
        if (existingEntity is not null)
        {
            entity.CreateDate = existingEntity.CreateDate;
            entity.CreateUserId = existingEntity.CreateUserId;
            entity.DeleteDate = existingEntity.DeleteDate;
            entity.DeleteUserId = existingEntity.DeleteUserId;
            _dbContext.Entry(existingEntity).CurrentValues.SetValues(entity);
            existingEntity.UpdateDate = DateTime.Now;
            await _dbContext.SaveChangesAsync();
        }
        return existingEntity;
    }

    public async Task<TEntity?> DeleteAsync(TEntity entity)
    {
        TEntity? existingEntity = await _dbContext.Set<TEntity>().FindAsync(entity.Id);
        if (existingEntity is not null)
        {
            existingEntity.DeleteDate = DateTime.Now;
            existingEntity.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
        return existingEntity;
    }

    public async Task<List<TEntity>> GetAllWithFilterAsync(Expression<Func<TEntity, bool>> filter) => await _dbContext.Set<TEntity>().Where(filter).ToListAsync();

    public IQueryable<TEntity> Query()
    {
        return _dbContext.Set<TEntity>().AsQueryable();
    }
}


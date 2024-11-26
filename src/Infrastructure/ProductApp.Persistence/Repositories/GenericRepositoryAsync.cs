using Microsoft.EntityFrameworkCore;
using ProductApp.Application.Interfaces.Repository;
using ProductApp.Domain.Common;
using ProductApp.Persistence.Context;

namespace ProductApp.Persistence.Repositories;

public class GenericRepositoryAsync<TEntity> : IGenericRepositoryAsync<TEntity> where TEntity : BaseEntity
{

    private readonly ApplicationDbContext dbContext;

    public GenericRepositoryAsync(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<TEntity>> GetAllAsync() => await dbContext.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetByIdAsync(long Id) => await dbContext.Set<TEntity>().FindAsync(Id);

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        entity.CreateUserId = 0;
        entity.CreateDate = DateTime.Now;
        await dbContext.Set<TEntity>().AddAsync(entity);
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var updatedEntity = await dbContext.Set<TEntity>().FindAsync(entity.Id);
        if (updatedEntity is not null)
        {
            dbContext.Entry(updatedEntity).State = EntityState.Detached;
            entity.CreateDate = updatedEntity.CreateDate;
            entity.CreateUserId = updatedEntity.CreateUserId;
            entity.DeleteDate = updatedEntity.DeleteDate;
            entity.DeleteUserId = updatedEntity.DeleteUserId;
            entity.UpdateUserId = 0;
            entity.UpdateDate = DateTime.Now;

            dbContext.Update(entity);
        }
        await dbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var deletedEntity = await dbContext.Set<TEntity>().FindAsync(entity.Id);
        if (deletedEntity is not null)
        {
            deletedEntity.DeleteUserId = 0;
            deletedEntity.DeleteDate = DateTime.Now;
            deletedEntity.IsDeleted = true;

            dbContext.Update(deletedEntity);
        }
        await dbContext.SaveChangesAsync();
        return entity;
    }
}

using ProductApp.Domain.Common;

namespace ProductApp.Application.Interfaces.Repositories;

public interface IGenericRepositoryAsync<TEntity, TId> where TEntity : BaseEntity<TId> where TId : notnull
{
    Task<List<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId Id);
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity?> UpdateAsync(TEntity entity);
    Task<TEntity?> DeleteAsync(TEntity entity);
    IQueryable<TEntity> Query(); 
}

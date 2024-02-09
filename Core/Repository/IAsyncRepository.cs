using Core.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repository;

public interface IAsyncRepository<TEntity>
    where TEntity : Entity
{
    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null,
                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, dynamic>> include = null,
                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                bool isTracking = false,
                                short limit = 100,
                                short page = 0);

    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> condition = null,
                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, dynamic>> include = null);

    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}

using Core.Entities.Common;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Core.Repository;

public interface IRepository<TEntity>
    where TEntity : Entity
{
    IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null,
                                Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, dynamic>> include = null,
                                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                bool isTracking = false,
                                short limit = 0,
                                short page = 0);

    TEntity? Get(Expression<Func<TEntity, bool>> condition = null,
                 Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, dynamic>> include = null);

    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Delete(TEntity entity);
}

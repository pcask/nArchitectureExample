using Core.Entities.Common;

namespace Core.Repository;

public interface IQuery<TEntity>
    where TEntity : Entity
{
    IQueryable<TEntity> Query();
}

﻿using Core.Entities.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Storage;
using System.Diagnostics;
using System.Linq.Expressions;

namespace Core.Repository.EFCore;

public abstract class Repository<TEntity>(DbContext context) : IRepository<TEntity>, IAsyncRepository<TEntity>, IQuery<TEntity>
    where TEntity : Entity
{
    public IDbContextTransaction BeginTransaction() => context.Database.BeginTransaction();
    public async Task<IDbContextTransaction> BeginTransactionAsync() => await context.Database.BeginTransactionAsync();

    public IQueryable<TEntity> Query()
    {
        return context.Set<TEntity>();
    }
    public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, dynamic>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool isTracking = false, short limit = 0, short page = 0)
    {
        var query = Query();

        if (!isTracking) query = query.AsNoTracking();
        if (filter is not null) query = query.Where(filter);
        if (include is not null) query = include(query);
        if (orderBy is not null) query = orderBy(query);

        query = query.Skip(page * limit).Take(limit);

        Debug.WriteLine(query.ToQueryString());

        return query;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, dynamic>> include = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, bool isTracking = false, short limit = 100, short page = 0)
    {
        var query = Query();

        if (!isTracking) query = query.AsNoTracking();
        if (filter is not null) query = query.Where(filter);
        if (include is not null) query = include(query);
        if (orderBy is not null) query = orderBy(query);

        query = query.Skip(page * limit).Take(limit);

        Debug.WriteLine(query.ToQueryString());

        return await query.ToListAsync();
    }
    public TEntity? Get(Expression<Func<TEntity, bool>> condition = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, dynamic>> include = null)
    {
        var query = Query().Where(condition);

        if (include is not null) query = include(query);

        return query.FirstOrDefault();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> condition = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, dynamic>> include = null)
    {
        var query = Query().Where(condition);

        if (include is not null) query = include(query);

        return await query.FirstOrDefaultAsync();
    }

    public TEntity Add(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Added;
        context.SaveChanges();

        return entity;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Added;
        await context.SaveChangesAsync();

        return entity;
    }

    public TEntity Update(TEntity entity)
    {
        // Detach(entity).GetAwaiter().GetResult();

        context.Entry(entity).State = EntityState.Modified;
        context.SaveChanges();

        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        // await Detach(entity);

        context.Entry(entity).State = EntityState.Modified;
        await context.SaveChangesAsync();

        return entity;
    }

    public void Delete(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Deleted;
        context.SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        context.Entry(entity).State = EntityState.Deleted;
        await context.SaveChangesAsync();
    }

    // Olurda bir entity'yi Tracked olarak ele alıp sonrasında bu entity'i update etmek istersek aşağıdaki gibi bir hata ile karşılaşırız;
    // "The instance of entity type ‘X’ cannot be tracked because another instance with the same key value for {‘Id’} is already being tracked."
    // Bu hatanın önüne geçmek için update method'larımız içerisinde, öncelikli olarak aşağıdaki Detach methodunu çağırabiliriz.
    private async Task Detach(TEntity entity)
    {
        if (entity is Entity<Guid>)
        {
            var foundedEntity = context.Set<TEntity>().Local.FirstOrDefault(e => Cast(e).Id == Cast(entity).Id);
            if (foundedEntity != null)
                context.Entry(foundedEntity).State = EntityState.Detached;
        }

        Entity<Guid> Cast(TEntity entity) => (Entity<Guid>)(object)entity;
    }
}

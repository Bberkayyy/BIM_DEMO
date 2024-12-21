using Core.Persistence.EntityBaseModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories;

public class EfRepositoryBase<TContext, TEntity, TId> : IRepositoryBase<TEntity, TId>, IRepositoryBaseAsync<TEntity, TId>, IQuery<TEntity> where TEntity : EntityBase<TId> where TContext : DbContext
{

    protected TContext Context { get; set; }

    public EfRepositoryBase(TContext context)
    {
        Context = context;
    }

    public IQueryable<TEntity> Query() => Context.Set<TEntity>();

    public TEntity Create(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
        Context.SaveChanges();
        return entity;
    }

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Add(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public void DeleteFromDatabase(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        Context.SaveChanges();
    }

    public async Task DeleteFromDatabaseAsync(TEntity entity)
    {
        Context.Set<TEntity>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public void Delete(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        Context.SaveChanges();
    }

    public async Task DeleteAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = Query();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);
        return query.ToList();
    }

    public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = Query();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);

        return await query.ToListAsync();
    }

    public TEntity? GetById(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = Query();
        if (include != null)
            query = include(query);
        return query.SingleOrDefault(x => x.Id != null && x.Id.Equals(id));
    }

    public async Task<TEntity?> GetByIdAsync(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity> query = Query();
        if (include != null)
            query = include(query);
        return await query.SingleOrDefaultAsync(x => x.Id != null && x.Id.Equals(id));
    }

    public TEntity? GetByFilter(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity?> query = Query();
        query = query.Where(predicate!);

        if (include != null)
            query = include(query!);

        return query.FirstOrDefault();
    }

    public async Task<TEntity?> GetByFilterAsync(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null)
    {
        IQueryable<TEntity?> query = Query();
        query = query.Where(predicate!);

        if (include != null)
            query = include(query!);

        return await query.FirstOrDefaultAsync();
    }

    public TEntity Update(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        Context.SaveChanges();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        Context.Set<TEntity>().Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
}

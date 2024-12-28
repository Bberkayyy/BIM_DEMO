using Core.Persistence.EntityBaseModel;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Repositories;

public interface IRepositoryBase<TEntity, TId> where TEntity : EntityBase<TId>
{
    TEntity Create(TEntity entity);
    TEntity Update(TEntity entity);
    void DeleteFromDatabase(TEntity entity);
    void Delete(TEntity entity);
    List<TEntity> GetAll(Expression<Func<TEntity, bool>>? predicate = null, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    TEntity? GetById(TId id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    TEntity? GetByFilter(Expression<Func<TEntity, bool>> predicate, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>? include = null);
    bool EntityExists(Expression<Func<TEntity, bool>> predicate);
}

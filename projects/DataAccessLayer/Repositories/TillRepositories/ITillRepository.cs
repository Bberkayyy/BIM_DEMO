using Core.Persistence.Repositories;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.TillRepositories;

public interface ITillRepository
{
    Till Create(Till entity);
    Till Update(Till entity);
    void Delete(Till entity);
    List<Till> GetAll(Expression<Func<Till, bool>>? predicate = null, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null);
    Till? GetById(int id, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null);
    Till? GetByFilter(Expression<Func<Till, bool>> predicate, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null);

    Task<Till> CreateAsync(Till entity);
    Task<Till> UpdateAsync(Till entity);
    Task DeleteAsync(Till entity);
    Task<List<Till>> GetAllAsync(Expression<Func<Till, bool>>? predicate = null, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null);
    Task<Till?> GetByIdAsync(int id, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null);
    Task<Till?> GetByFilterAsync(Expression<Func<Till, bool>> predicate, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null);
}

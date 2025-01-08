using DataAccessLayer.Context;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories.TillRepositories;

public class TillRepository : ITillRepository
{
    private readonly BaseDbContext _context;

    public TillRepository(BaseDbContext context)
    {
        _context = context;
    }

    public Till Create(Till entity)
    {
        _context.Set<Till>().Add(entity);
        _context.SaveChanges();
        return entity;
    }

    public async Task<Till> CreateAsync(Till entity)
    {
        _context.Set<Till>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public void Delete(Till entity)
    {
        _context.Set<Till>().Remove(entity);
        _context.SaveChanges();
    }

    public async Task DeleteAsync(Till entity)
    {
        _context.Set<Till>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public List<Till> GetAll(Expression<Func<Till, bool>>? predicate = null, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null)
    {
        IQueryable<Till> query = _context.Set<Till>();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);
        return query.ToList();
    }

    public async Task<List<Till>> GetAllAsync(Expression<Func<Till, bool>>? predicate = null, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null)
    {
        IQueryable<Till> query = _context.Set<Till>();

        if (predicate != null)
            query = query.Where(predicate);
        if (include != null)
            query = include(query);
        return await query.ToListAsync();
    }

    public Till? GetByFilter(Expression<Func<Till, bool>> predicate, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null)
    {
        IQueryable<Till> query = _context.Set<Till>().AsNoTracking();
        query = query.Where(predicate);

        if (include != null)
            query = include(query);
        return query.FirstOrDefault();
    }

    public Task<Till?> GetByFilterAsync(Expression<Func<Till, bool>> predicate, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null)
    {
        IQueryable<Till> query = _context.Set<Till>().AsNoTracking();
        query = query.Where(predicate);

        if (include != null)
            query = include(query);
        return query.FirstOrDefaultAsync();
    }

    public Till? GetById(int id, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null)
    {
        IQueryable<Till> query = _context.Set<Till>();
        if (include != null) query = include(query);
        return query.SingleOrDefault(x => x.Id == id);
    }

    public async Task<Till?> GetByIdAsync(int id, Func<IQueryable<Till>, IIncludableQueryable<Till, object>>? include = null)
    {
        IQueryable<Till> query = _context.Set<Till>();
        if (include != null) query = include(query);
        return await query.SingleOrDefaultAsync(x => x.Id == id);
    }

    public Till Update(Till entity)
    {
        _context.Set<Till>().Update(entity);
        _context.SaveChanges();
        return entity;
    }

    public async Task<Till> UpdateAsync(Till entity)
    {
        _context.Set<Till>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}

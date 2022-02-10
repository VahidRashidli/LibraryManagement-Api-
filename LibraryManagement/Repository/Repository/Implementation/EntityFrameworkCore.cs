using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DomainModels.Models.Base;
using Microsoft.EntityFrameworkCore;
using Repository.DAL;
using Repository.Repository.Abstarction;

namespace Repository.Repository.Implementation
{
    public class EntityFrameworkCore<T> : IRepository<T>
        where T:class,IEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly IQueryable<T> _iQuerable;
        public EntityFrameworkCore(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _iQuerable = _dbContext.Set<T>();
        }
        public async Task<bool> AddAsync(T item)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> AddRangeAsync(IList<T> items)
        {
            try
            {
                await _dbContext.Set<T>().AddRangeAsync(items);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                T item = await _dbContext.Set<T>().FindAsync(id);
                item.IsDeleted = true;
                item.DeletedDate = DateTime.Now;
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> UpdateAsync(T item)
        {
            try
            {
                _dbContext.Set<T>().Update(item);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public IQueryable<T> GetIQuerable()
        {
            return _iQuerable.Where(i => !i.IsDeleted).AsNoTracking();
        }
        public async Task<IList<T>> GetAllFilteredAsync(Expression<Func<T, bool>> expression,
        IList<string> objectsToInclude=null)
        {
            if (objectsToInclude != null)
            {
                foreach (string item in objectsToInclude)
                {
                    _iQuerable.Include(item);
                }
            }
            return await _iQuerable.Where(expression).ToListAsync();
        }
    }
}

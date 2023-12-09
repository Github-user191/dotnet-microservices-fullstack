using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using Ordering.Infrastructure.Data;

namespace Ordering.Infrastructure.Repositories {
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase {

        protected readonly ApplicationDbContext _context;

        public RepositoryBase(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<T> GetByIdAsync(int id) {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity) {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> entities) {
            _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
            return entities;
        }

        public async Task UpdateAsync(T entity) {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity) {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAllAsync(IEnumerable<T> entities) {
           _context.Set<T>().RemoveRange(entities);
           await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync() {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate) {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        // string includeString
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true) {
            IQueryable<T> query = _context.Set<T>();

            if (disableTracking) {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrWhiteSpace(includeString)) {
                query = query.Where(predicate);
            }

            if (predicate != null) {
                query = query.Where(predicate);
            }

            if (orderBy != null) {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        // List<Expression<Func<T, object>>> includes
        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true) {
            IQueryable<T> query = _context.Set<T>();

            if(disableTracking) {
                query = query.AsNoTracking();
            }

            if(includes != null) {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null) {
                query = query.Where(predicate);
            }

            if(orderBy != null) {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }




    }
}

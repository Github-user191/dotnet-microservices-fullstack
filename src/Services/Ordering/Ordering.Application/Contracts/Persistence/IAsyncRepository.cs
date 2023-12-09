using System.Linq.Expressions;
using Ordering.Domain.Common;

namespace Ordering.Application.Contracts.Persistence; 

// Changes should not be made to the Contracts/Persistance, the implementation must be changed in the Infrastructure layer and 
// leave the Application layer unaffected
// These interfaces are defined in the Application layer and implemented in the Infrastructure later
// Not interested in external libraries within the Application layer (Core layer)
public interface IAsyncRepository<T> where T : EntityBase {
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true);
    Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true);
    Task<T> GetByIdAsync(int id);
    Task<T> AddAsync(T entity);
    Task<IEnumerable<T>> AddAllAsync(IEnumerable<T> entities);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
    Task DeleteAllAsync(IEnumerable<T> entities);
}
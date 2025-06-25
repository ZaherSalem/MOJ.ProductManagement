using System.Linq.Expressions;

namespace MOJ.ProductManagement.Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Entities { get; }

        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task<T> AddAsync(T entity);
        Task<T> AddListAsync(List<T> entity);

        Task UpdateAsync(T entity);
        Task<T> UpdateListAsync(List<T> entites);
        Task DeleteAsync(T entity);
        Task DeleteRangeAsync(IEnumerable<T> entities);
        Task<List<T>> FindByAsync(Expression<Func<T, bool>> condition);
        Task<T> GetAsync(Expression<Func<T, bool>> condition);
        IQueryable<T> GetQueryable();

        Task<bool> AnyAsync(Expression<Func<T, bool>> condition);
    }
}

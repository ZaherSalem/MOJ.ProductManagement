using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Domain.Interfaces;
using System.Linq.Expressions;

namespace MOJ.ProductManagement.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbContext _dbContext;
        private DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected DbSet<T> DbSet
        {
            get => _dbSet ?? (_dbSet = _dbContext.Set<T>());
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();

        public async Task<T> AddAsync(T entity)
        {
            await DbSet.AddAsync(entity);
            _dbContext.SaveChanges();
            return entity;
        }
        public async Task<T> AddListAsync(List<T> entites)
        {
            await DbSet.AddRangeAsync(entites);
            _dbContext.SaveChanges();
            return entites.FirstOrDefault();
        }
        public Task UpdateAsync(T entity)
        {
            T exist = DbSet.Find(entity.Id);
            _dbContext.Entry(exist).CurrentValues.SetValues(entity);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }

        public Task DeleteAsync(T entity)
        {
            DbSet.Remove(entity);
            _dbContext.SaveChanges();
            return Task.CompletedTask;
        }
        public async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
            _dbContext.SaveChanges();
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await DbSet.FindAsync(id);
        }
        public async Task<T> UpdateListAsync(List<T> entites)
        {
            DbSet.UpdateRange(entites);
            _dbContext.SaveChanges();
            return entites.FirstOrDefault();
        }

        public async Task<List<T>> FindByAsync(Expression<Func<T, bool>> condition)
        {
            return await DbSet.Where(condition).ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> condition)
        {
            return await DbSet.FirstOrDefaultAsync(condition);
        }


        public IQueryable<T> GetQueryable()
        {
            return DbSet.AsQueryable<T>();
        }
    }
}
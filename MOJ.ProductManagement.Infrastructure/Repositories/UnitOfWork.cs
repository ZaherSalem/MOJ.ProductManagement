using Microsoft.EntityFrameworkCore;
using MOJ.ProductManagement.Domain.Repositories;
using MOJ.ProductManagement.Infrastructure.Data;
using System.Collections;
using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Domain.Interfaces;

namespace MOJ.ProductManagement.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbFactory<ApplicationDbContext> _DbFactory;
        private Hashtable _repositories;
        private bool disposed;
     
        public UnitOfWork(DbFactory<ApplicationDbContext> DbFactory)
        {
            _DbFactory = DbFactory;
            _repositories = new Hashtable();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
            {
                if (disposing)
                {
                    _DbFactory.Dispose();
                }
            }
            //dispose unmanaged resources
            disposed = true;
        }
        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }
            DbContext _dbContext;
            _dbContext = _DbFactory.DbContext;

            var repository = new Repository<TEntity>(_dbContext);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }
    }
}

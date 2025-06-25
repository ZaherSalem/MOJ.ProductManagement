using MOJ.ProductManagement.Domain.Entities;
using MOJ.ProductManagement.Domain.Interfaces;

namespace MOJ.ProductManagement.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity;
    }
}

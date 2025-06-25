using Microsoft.EntityFrameworkCore;

namespace MOJ.ProductManagement.Infrastructure.Data
{
    public class DbFactory<T> : IDisposable where T : DbContext
    {
        private bool _disposed;
        private Func<T> _instanceFunc;
        private T _dbContext;
        public T DbContext => _dbContext ?? (_dbContext = _instanceFunc.Invoke());

        public DbFactory(Func<T> dbContextFactory)
        {
            _instanceFunc = dbContextFactory;
        }

        public void Dispose()
        {
            if (!_disposed && _dbContext != null)
            {
                _disposed = true;
                _dbContext.Dispose();
            }
        }
    }
}
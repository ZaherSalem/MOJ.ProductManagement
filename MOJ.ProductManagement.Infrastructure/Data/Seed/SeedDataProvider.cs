using MOJ.ProductManagement.Domain.Entities;

namespace MOJ.ProductManagement.Infrastructure.Data.Seed
{
    public interface ISeedDataProvider<T, ET> where T : Entity<ET>
    {
        IEnumerable<T> GetSeedData();
    }
    public class SeedDataProvider<T, ET> : ISeedDataProvider<T, ET> where T : Entity<ET>
    {
        private readonly Func<IEnumerable<T>> _getSeedData;

        public SeedDataProvider(Func<IEnumerable<T>> getSeedData)
        {
            _getSeedData = getSeedData;
        }

        public IEnumerable<T> GetSeedData()
        {
            return _getSeedData();
        }
    }
}

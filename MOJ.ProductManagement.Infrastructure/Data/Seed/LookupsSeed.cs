using MOJ.ProductManagement.Domain.Entities;

namespace MOJ.ProductManagement.Infrastructure.Data.Seed
{
    public static class LookupsSeed
    {
        public static IEnumerable<Lookup> GetData() => new List<Lookup>
        {
          new Lookup{
            Id = 1,
            Name = "Quantity Per Unit",
            ParentId = null
          },
          new Lookup{
            Id = 10,
            Name = "Kilo",
            ParentId = 1
          },
          new Lookup{
            Id = 11,
            Name = "Box",
            ParentId = 1
          },
          new Lookup{
            Id = 12,
            Name = "Can",
            ParentId = 1
          },
          new Lookup{
            Id = 13,
            Name = "Liter",
            ParentId = 1
          },
          new Lookup{
            Id = 14,
            Name = "Bottle",
            ParentId = 1
          },

        };
    }
}
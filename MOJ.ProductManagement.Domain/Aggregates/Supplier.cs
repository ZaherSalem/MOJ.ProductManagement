using MOJ.ProductManagement.Domain.Entities;

namespace MOJ.ProductManagement.Domain.Aggregates
{
    public class Supplier : AggregateRoot<int>
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

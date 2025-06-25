using MOJ.ProductManagement.Domain.Aggregates;

namespace MOJ.ProductManagement.Domain.Entities
{
    public class Product : AggregateRoot<int>
    {
        public string Name { get; private set; }
        public int QuantityPerUnitId { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int ReorderLevel { get; private set; }
        public int SupplierId { get; private set; } // Foreign Key
        public int UnitsInStock { get; private set; }
        public int UnitsOnOrder { get; private set; }

        public bool NeedsReorder() => UnitsInStock < ReorderLevel;

        public virtual Supplier Supplier { get; set; } // Navigation Property
        public virtual Lookup QuantityPerUnit { get; set; } // Navigation Property

    }
}

using Microsoft.Xrm.Sdk;
using MOJ.ProductManagement.Domain.Aggregates;
using MOJ.ProductManagement.Domain.ValueObjects;

namespace MOJ.ProductManagement.Domain.Entities
{
    public class Product : AggregateRoot<int>
    {
        public string Name { get; private set; }
        public QuantityPerUnit QuantityPerUnit { get; private set; }
        public Money UnitPrice { get; private set; }
        public int ReorderLevel { get; private set; }
        public int SupplierId { get; private set; } // Foreign Key
        public int UnitsInStock { get; private set; }
        public int UnitsOnOrder { get; private set; }

        public bool NeedsReorder() => UnitsInStock < ReorderLevel;

        public Supplier Supplier { get; set; } // Navigation Property

    }
}

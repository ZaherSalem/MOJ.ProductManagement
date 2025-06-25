namespace MOJ.ProductManagement.Application.DTOs.Product
{
    public record ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int QuantityPerUnitId { get; private set; }
        public int ReorderLevel { get; init; }
        public int SupplierId { get; init; }
        public decimal UnitPrice { get; init; }
        public int UnitsInStock { get; init; }
        public int UnitsOnOrder { get; init; }

        public string SupplierName { get; init; }
        public string QuantityPerUnitName { get; private set; }
    }
}

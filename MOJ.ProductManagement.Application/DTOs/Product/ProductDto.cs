namespace MOJ.ProductManagement.Application.DTOs.Product
{
    public class ProductDto
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public int QuantityPerUnitId { get; private set; }
        public int ReorderLevel { get; init; }
        public int SupplierId { get; init; }
        public decimal UnitPrice { get; init; }
        public int UnitsInStock { get; init; }
        public int UnitsOnOrder { get; init; }
        public DateTime LastOrderDate { get; set; }
        public string SupplierName { get; init; }
        public string QuantityPerUnitName { get; private set; }
    }
}

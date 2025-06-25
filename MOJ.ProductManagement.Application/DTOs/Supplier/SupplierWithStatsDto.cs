namespace MOJ.ProductManagement.Application.DTOs.Supplier
{
    public class SupplierWithStatsDto
    {
        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int ProductCount { get; set; }
        public decimal TotalInventoryValue { get; set; }
    }
}

namespace MOJ.ProductManagement.Application.DTOs
{ 
    public class ProductStatisticsDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int TotalStock { get; set; }
        public int TotalOrders { get; set; }
        public decimal TotalValue { get; set; }
        public bool NeedsReorder { get; set; }
    }
}

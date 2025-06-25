namespace MOJ.ProductManagement.Application.DTOs
{
    public class ProductFilter
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SearchTerm { get; set; }
        public int? SupplierId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public bool? NeedsReorder { get; set; }
    }
}

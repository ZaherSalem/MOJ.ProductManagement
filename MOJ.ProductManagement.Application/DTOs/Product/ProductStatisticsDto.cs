using MOJ.ProductManagement.Application.DTOs.Product;
using MOJ.ProductManagement.Application.DTOs.Supplier;

namespace MOJ.ProductManagement.Application.DTOs
{ 
    public class ProductStatisticsDto
    {
        public List<ProductDto>? ProductsToReorder { get; set; }
        public SupplierDto? LargestSupplier { get; set; }
        public ProductDto? ProductWithMinimumOrders { get; set; }
    }
}

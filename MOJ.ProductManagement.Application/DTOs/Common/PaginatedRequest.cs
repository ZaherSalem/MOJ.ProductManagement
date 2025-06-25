namespace MOJ.ProductManagement.Application.DTOs.Common
{
    public class PaginatedRequest
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string? SearchValue { get; set; } = string.Empty;
    }
}

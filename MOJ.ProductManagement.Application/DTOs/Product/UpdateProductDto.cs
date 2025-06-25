using System.ComponentModel.DataAnnotations;

namespace MOJ.ProductManagement.Application.DTOs.Product
{
    /// <summary>
    /// Data transfer object for updating an existing product
    /// </summary>
    public class UpdateProductDto
    {
        /// <summary>
        /// Product ID (must match route ID)
        /// </summary>
        [Required]
        public int Id { get; set; }

        /// <summary>
        /// Name of the product (required, max 100 characters)
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Unit of measurement for the product
        /// </summary>
        [Required]
        public int QuantityPerUnitId { get; set; }

        /// <summary>
        /// Minimum stock level before reordering (must be >= 0)
        /// </summary>
        [Range(0, int.MaxValue)]
        public int ReorderLevel { get; set; }

        /// <summary>
        /// ID of the supplier (required)
        /// </summary>
        [Required]
        public int SupplierId { get; set; }

        /// <summary>
        /// Price per unit (must be positive)
        /// </summary>
        [Range(0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Number of units currently in stock (must be >= 0)
        /// </summary>
        [Range(0, int.MaxValue)]
        public int UnitsInStock { get; set; }

        /// <summary>
        /// Number of units currently on order (must be >= 0)
        /// </summary>
        [Range(0, int.MaxValue)]
        public int UnitsOnOrder { get; set; }
    }
}
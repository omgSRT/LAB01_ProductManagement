using System.ComponentModel.DataAnnotations;

namespace LAB01_ProductManagementAPI.DTO
{
    public class ProductRequest
    {
        [Required]
        [StringLength(40)]
        public string? ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int UnitsInStock { get; set; }
        [Required]
        [Range (0.01, double.MaxValue)]
        public decimal UnitPrice { get; set; }
    }
}

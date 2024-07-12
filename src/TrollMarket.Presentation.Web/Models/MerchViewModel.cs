using System.ComponentModel.DataAnnotations;

namespace TrollMarket.Presentation.Web.Models
{
    public class MerchViewModel
    {
        public int? MerchandiseId { get; set; }

        [Required]
        public string ProductName { get; set; } = null!;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string Category { get; set; } = null!;

        public string? Description { get; set; }
        public string SellerName { get; set; } = null!;

        public string? FullName { get; set; } 

        [Required]
        public bool IsDiscontinued { get; set; }
    }
}

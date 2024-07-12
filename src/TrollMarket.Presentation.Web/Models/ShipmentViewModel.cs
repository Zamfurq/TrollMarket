using System.ComponentModel.DataAnnotations;

namespace TrollMarket.Presentation.Web.Models
{
    public class ShipmentViewModel
    {
        public int ShipmentId { get; set; }

        [Required]
        public string ShipperName { get; set; } = null!;

        [Required]
        [Range(1, int.MaxValue)]  
        
        public decimal Price { get; set; }

        [Required]
        public bool IsService { get; set; } = true;
    }
}

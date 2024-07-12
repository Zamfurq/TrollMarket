using System.ComponentModel.DataAnnotations;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Presentation.Web.Models
{
    public class CartViewModel
    {
        public string? BuyerName { get; set; }
        public int MerchaniseId { get; set; }

        [Required]
        public int ShipmentId { get; set; }
        [Required]
        [Range(1,int.MaxValue)]
        public int Quantity { get; set; }

        public Merchandise? Merchandise { get; set; }

        public Shipment? Shipment { get; set; }

        public string? SellerName { get; set; }
    }
}

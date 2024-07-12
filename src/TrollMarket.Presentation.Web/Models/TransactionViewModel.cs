using TrollMarket.DataAccess.Models;

namespace TrollMarket.Presentation.Web.Models
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        public DateTime TransactionDate { get; set; }

        public string SellerName { get; set; }

        public string BuyerName { get; set; }

        public int Quantity { get; set; }

        public decimal ShipmentPrice { get; set; }

        public decimal Price { get; set; }

        public Merchandise Merchandise { get; set; }

        public Shipment Shipment { get; set; }
    }
}

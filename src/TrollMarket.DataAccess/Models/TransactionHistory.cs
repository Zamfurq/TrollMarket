using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models
{
    public partial class TransactionHistory
    {
        public int TransactionId { get; set; }
        public string SellerName { get; set; } = null!;
        public string BuyerName { get; set; } = null!;
        public int MerchandiseId { get; set; }
        public int ShipmentId { get; set; }
        public decimal ShipmentPrice { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime TransactionDate { get; set; }

        public virtual Account BuyerNameNavigation { get; set; } = null!;
        public virtual Merchandise Merchandise { get; set; } = null!;
        public virtual Account SellerNameNavigation { get; set; } = null!;
        public virtual Shipment Shipment { get; set; } = null!;
    }
}

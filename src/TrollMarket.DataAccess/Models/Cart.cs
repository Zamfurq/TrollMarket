using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models
{
    public partial class Cart
    {
        public string BuyerName { get; set; } = null!;
        public int MerchaniseId { get; set; }
        public int ShipmentId { get; set; }
        public int Quantity { get; set; }

        public virtual Account BuyerNameNavigation { get; set; } = null!;
        public virtual Merchandise Merchanise { get; set; } = null!;
        public virtual Shipment Shipment { get; set; } = null!;
    }
}

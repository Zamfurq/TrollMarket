using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            Carts = new HashSet<Cart>();
            TransactionHistories = new HashSet<TransactionHistory>();
        }

        public int ShipmentId { get; set; }
        public string ShipperName { get; set; } = null!;
        public decimal Price { get; set; }
        public bool IsService { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}

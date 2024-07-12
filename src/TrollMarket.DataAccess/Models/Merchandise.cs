using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models
{
    public partial class Merchandise
    {
        public Merchandise()
        {
            Carts = new HashSet<Cart>();
            TransactionHistories = new HashSet<TransactionHistory>();
        }

        public int MerchandiseId { get; set; }
        public string ProductName { get; set; } = null!;
        public decimal Price { get; set; }
        public string Category { get; set; } = null!;
        public string? Description { get; set; }
        public string SellerName { get; set; } = null!;
        public bool IsDiscontinued { get; set; }

        public virtual Account SellerNameNavigation { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistories { get; set; }
    }
}

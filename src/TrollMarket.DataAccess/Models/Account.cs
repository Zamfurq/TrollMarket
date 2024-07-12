using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models
{
    public partial class Account
    {
        public Account()
        {
            Carts = new HashSet<Cart>();
            Merchandises = new HashSet<Merchandise>();
            TransactionHistoryBuyerNameNavigations = new HashSet<TransactionHistory>();
            TransactionHistorySellerNameNavigations = new HashSet<TransactionHistory>();
            UserRoles = new HashSet<UserRole>();
        }

        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual UserDetail UserDetail { get; set; } = null!;
        public virtual ICollection<Cart> Carts { get; set; }
        public virtual ICollection<Merchandise> Merchandises { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistoryBuyerNameNavigations { get; set; }
        public virtual ICollection<TransactionHistory> TransactionHistorySellerNameNavigations { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}

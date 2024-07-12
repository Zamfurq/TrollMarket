using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models
{
    public partial class UserDetail
    {
        public string Username { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? LastName { get; set; }
        public string Address { get; set; } = null!;
        public decimal Balance { get; set; }

        public virtual Account UsernameNavigation { get; set; } = null!;
    }
}

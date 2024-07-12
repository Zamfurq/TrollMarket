using System;
using System.Collections.Generic;

namespace TrollMarket.DataAccess.Models
{
    public partial class UserRole
    {
        public string Username { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual Account UsernameNavigation { get; set; } = null!;
    }
}

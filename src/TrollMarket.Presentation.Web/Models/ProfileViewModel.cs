using System.ComponentModel.DataAnnotations;

namespace TrollMarket.Presentation.Web.Models
{
    public class ProfileViewModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Role { get; set; }

        public string Address { get; set; }

        [Required]
        [Range(0.0,Double.MaxValue)]
        public decimal Balance { get; set; }
    }
}

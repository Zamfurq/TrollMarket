using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Web.Validation;

namespace TrollMarket.Presentation.Web.Models
{
    public class AdminRegisterViewModel
    {
        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        [UniqueAdmin]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using TrollMarket.Presentation.Web.Validation;

namespace TrollMarket.Presentation.Web.Models
{
    public class UserRegisterViewModel
    {
        public string Role { get; set; }

        [StringLength(maximumLength: 50, MinimumLength = 3)]
        [Required]
        [UniqueUsername("Role")]
        public string Username { get; set; }

        [Required]
        [NewRolePassword]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string FirstName { get; set; }   

        public string? LastName { get; set; }

        [Required]
        public string Address { get; set; }
    }
}

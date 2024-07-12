using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarket.DataAccess.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace TrollMarket.Presentation.Web.Models
{
    public class UserLoginViewModel
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        [Required]
        public Role Role { get; set; }

        public List<SelectListItem>? Roles { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Presentation.Web.Validation
{
    public class UniqueAdminAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var username = ((string)value).ToLower();
            var dbContext = (TrollMarketContext)validationContext.GetService(typeof(TrollMarketContext));

            var nameExists = dbContext.UserRoles.Any(a => a.Username.ToLower().Equals(username) && a.Role.Equals("Admin"));

            if (nameExists)
            {
                return new ValidationResult("Username is already taken in admin role");
            }
            else
            {
                return ValidationResult.Success;
            }
        }
    }
}

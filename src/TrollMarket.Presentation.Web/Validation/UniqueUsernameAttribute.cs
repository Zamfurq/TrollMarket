using System.ComponentModel.DataAnnotations;
using TrollMarket.DataAccess.Models;
using TrollMarket.DataAccess.Models.Enum;

namespace TrollMarket.Presentation.Web.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class UniqueUsernameAttribute : ValidationAttribute

    {
        readonly string _role;

        public UniqueUsernameAttribute(string role) 
        {
            _role = role;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var username = ((string)value).ToLower();
            var dbContext = (TrollMarketContext)validationContext.GetService(typeof(TrollMarketContext));

            var dependentProperty = validationContext.ObjectType.GetProperty(_role);

            if (dependentProperty == null)
            {
                throw new ArgumentException($"Property {_role} not found.");
            }

            var theRole= dependentProperty.GetValue(validationContext.ObjectInstance);

            var nameExists = dbContext.UserRoles.Any(a => a.Username.ToLower().Equals(username) && a.Role.Equals(theRole));

            if (nameExists)
            {
                return new ValidationResult("Username is already taken in this role");
            } else
            {
                return ValidationResult.Success;
            }
        }

    }
}

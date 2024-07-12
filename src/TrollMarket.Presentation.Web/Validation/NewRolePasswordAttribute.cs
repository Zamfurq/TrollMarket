using System.ComponentModel.DataAnnotations;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Validation
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class NewRolePasswordAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var password = ((string)value);
            var dbContext = (TrollMarketContext)validationContext.GetService(typeof(TrollMarketContext));

            var currentObject = (UserRegisterViewModel)validationContext.ObjectInstance;
            var currentUsername = currentObject.Username;
            var currentAccount = dbContext.Accounts.FirstOrDefault(a => a.Username == currentUsername);

            if (currentAccount != null)
            {
                bool correctPassword = BCrypt.Net.BCrypt.Verify(password, currentAccount.Password);

                if (!correctPassword)
                {
                    return new ValidationResult("Password is incorrect");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }
            else
            {
                return ValidationResult.Success;

            }



        }
    }
}

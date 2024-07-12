using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Security.Claims;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;
using TrollMarket.DataAccess.Models.Enum;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Services
{
    public class AuthService
    {
        private readonly IAccountRepository _accountRepository;

        public AuthService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }


        public UserDetail GetUserDetail(string username)
        {
            UserDetail userDetail = _accountRepository.GetUserDetail(username);
            return userDetail;
        }

        public void Register(UserRegisterViewModel vm)
        {
            Role theRole;
            Enum.TryParse(vm.Role, out theRole);
            var userCheck = _accountRepository.GetUserDetail(vm.Username);
            if (userCheck == null) {
                var account = new Account
                {
                    Username = vm.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                };
                var userRole = new UserRole
                {
                    Username = vm.Username,
                    Role = vm.Role.ToString(),
                };
                var userDetail = new UserDetail
                {
                    Username = vm.Username,
                    FirstName = vm.FirstName,
                    LastName = vm.LastName,
                    Address = vm.Address,
                    Balance = 0
                };
                _accountRepository.RegisterUser(account,userDetail,userRole);
            } else
            {
                var userRole = new UserRole
                {
                    Username = vm.Username,
                    Role = vm.Role.ToString(),
                };
                _accountRepository.RegisterUser(null,null,userRole);
            }
            
        }

        public AuthenticationTicket LoginUser(UserLoginViewModel vm)
        {
            var userPassword = _accountRepository.GetAccount(vm.UserName, vm.Role);
            bool isCorrectPassword = BCrypt.Net.BCrypt.Verify(vm.Password, userPassword.Password);
            if (!isCorrectPassword) {
                throw new Exception("Username or Password is incorrect");
            } else if (userPassword == null)
            {
                throw new Exception("Your role is incorrect");
            }

            ClaimsPrincipal principal;

            principal = GetPrincipal(userPassword);

            AuthenticationTicket authenticationTicket = GetAuthenticationTicket(principal);

            return authenticationTicket;
        }

        private ClaimsPrincipal GetPrincipal(Account account)
        {
            var claims = new List<Claim> {
                new Claim("username", account.Username),
                new Claim(ClaimTypes.Role, account.UserRoles.Select(u => u.Role).First())
            };

            ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            return new ClaimsPrincipal(identity);
        }

        private AuthenticationTicket GetAuthenticationTicket(ClaimsPrincipal principal)
        {
            AuthenticationProperties authenticationProperties = new AuthenticationProperties
            {
                IssuedUtc = DateTime.Now,
                ExpiresUtc = DateTime.Now.AddMinutes(30)
            };

            AuthenticationTicket authenticationTicket = new AuthenticationTicket(principal, authenticationProperties,
                                                                                CookieAuthenticationDefaults.AuthenticationScheme);

            return authenticationTicket;
        }

        public List<SelectListItem> GetRoles()
        {
            var roles = Enum.GetValues(typeof(Role));

            List<SelectListItem> result = new List<SelectListItem>();

            foreach (Role role in roles)
            {
                result.Add(new SelectListItem
                {
                    Text = role.ToString(),
                    Value = role.ToString()
                });
            }

            return result;
        }

        public UserRegisterViewModel GetUser(string username)
        {
            UserDetail userDetail = GetUserDetail(username);
            return new UserRegisterViewModel
            {
                Address = userDetail.Address,
                FirstName = userDetail.FirstName,
                LastName = userDetail.LastName
            };
        }
    }
}

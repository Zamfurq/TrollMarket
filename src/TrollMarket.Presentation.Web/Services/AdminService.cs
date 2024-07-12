using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Services
{
    public class AdminService
    {
        private readonly IAccountRepository _accountRepository;

        public AdminService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public void InsertAdmin(AdminRegisterViewModel vm)
        {
            var userCheck = _accountRepository.GetUserDetail(vm.Username);
            if (userCheck == null)
            {
                var account = new Account
                {
                    Username = vm.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                };
                var userRole = new UserRole
                {
                    Username = vm.Username,
                    Role = "Admin",
                };
                _accountRepository.RegisterAdmin(account,userRole);
            }
            else
            {
                var userRole = new UserRole
                {
                    Username = vm.Username,
                    Role = "Admin",
                };
                _accountRepository.RegisterAdmin(null, userRole);
            }
        } 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;
using TrollMarket.DataAccess.Models.Enum;

namespace TrollMarket.Business.Interfaces
{
    public interface IAccountRepository
    {
        //public dynamic GetUser(string username, Role role);

        public Account GetAccount(string username, Role role);
        public void RegisterUser(Account? account, UserDetail? userDetail, UserRole userRole);

        public void RegisterAdmin(Account? account, UserRole userRole);

        public UserDetail GetUserDetail(string username);

        public void UpdateUserDetail(UserDetail userDetail);

        public List<TransactionHistory> GetTransactionsBySeller(int pageNumber, int pageSize, string username);

        public List<TransactionHistory> GetTransactionsByBuyer(int pageNumber, int pageSize, string username);

        public int CountTransactionByBuyer(string username);

        public int CountTransactionBySeller(string username);

        public List<Account> GetAllAccounts();
    }
}

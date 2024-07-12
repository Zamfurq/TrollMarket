using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;
using TrollMarket.DataAccess.Models.Enum;

namespace TrollMarket.Business.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly TrollMarketContext _dbContext;

        public AccountRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public dynamic GetUser(string username, Role role)
        //{
        //    var account = (from acc in _dbContext.Accounts
        //                      join userRole in _dbContext.UserRoles on acc.Username equals userRole.Username
        //                      where acc.Username == username && userRole.Role == role.ToString()
        //                      select new { Account = acc, UserRole = userRole}).FirstOrDefault();
            
            
        //    return account;
        //}

        public Account? GetAccount(string username, Role role)
        {
            var account = _dbContext.Accounts
                .Include(a => a.UserRoles.Where(u => u.Role.Equals(role.ToString())))
                .Where(a => a.Username == username).FirstOrDefault();

            return account;
        }

        public void RegisterUser(Account? account, UserDetail? userDetail, UserRole userRole)
        {
            if (account != null)
            {
                _dbContext.Accounts.Add(account);
                _dbContext.UserDetails.Add(userDetail);
                _dbContext.UserRoles.Add(userRole);
                _dbContext.SaveChanges();
            } 
            else
            {
                _dbContext.UserRoles.Add(userRole);
                _dbContext.SaveChanges();
            }
        }

        public void RegisterAdmin(Account? account, UserRole userRole)
        {
            if (account != null)
            {
                _dbContext.Accounts.Add(account);
                _dbContext.UserRoles.Add(userRole);
                _dbContext.SaveChanges();
            }
            else
            {
                _dbContext.UserRoles.Add(userRole);
                _dbContext.SaveChanges();
            }
        }

        public UserDetail? GetUserDetail(string username)
        {
            return _dbContext.UserDetails.FirstOrDefault(u => u.Username.Equals(username))
                ?? null;
        }

        public void UpdateUserDetail(UserDetail userDetail)
        {
            if(userDetail.Username == null)
            {
                throw new Exception("Username not found");
            }
            _dbContext.UserDetails.Update(userDetail);
            _dbContext.SaveChanges();
        }

        public List<TransactionHistory> GetTransactionsBySeller(int pageNumber, int pageSize,string username)
        {
            var query = from transactionHistory in _dbContext.TransactionHistories
                        where transactionHistory.SellerName.Equals(username)
                        select transactionHistory;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountTransactionBySeller(string username)
        {
            var query = from transactionHistory in _dbContext.TransactionHistories
                        where transactionHistory.SellerName.Equals(username)
                        select transactionHistory;

            return query.Count();
        }

        public List<TransactionHistory> GetTransactionsByBuyer(int pageNumber, int pageSize, string username)
        {
            var query = from transactionHistory in _dbContext.TransactionHistories
                        where transactionHistory.BuyerName.Equals(username)
                        select transactionHistory;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountTransactionByBuyer(string username)
        {
            var query = from transactionHistory in _dbContext.TransactionHistories
                        where transactionHistory.BuyerName.Equals(username)
                        select transactionHistory;

            return query.Count();
        }

        public List<Account> GetAllAccounts()
        {
            return _dbContext.Accounts.ToList();
        }

    }
}

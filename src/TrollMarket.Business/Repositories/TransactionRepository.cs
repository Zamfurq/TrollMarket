using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly TrollMarketContext _dbContext;

        public TransactionRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<TransactionHistory> GetAllTransactions(int pageNumber, int pageSize, string sellerName, string buyerName)
        {
            var query = from transactionHistory in _dbContext.TransactionHistories
                        where (sellerName == "" || transactionHistory.SellerName.Contains(sellerName))
                        && (buyerName == "" || transactionHistory.BuyerName.Contains(buyerName))
                        select transactionHistory;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountTransactions(string sellerName, string buyerName)
        {
            var query = from transactionHistory in _dbContext.TransactionHistories
                        where (sellerName == "" || transactionHistory.SellerName.Contains(sellerName))
                        && (buyerName == "" || transactionHistory.BuyerName.Contains(buyerName))
                        select transactionHistory;

            return query.Count();
        }

        public void AddToTransaction(TransactionHistory transaction)
        {
            _dbContext.TransactionHistories.Add(transaction);
            _dbContext.SaveChanges();
        }
    }
}

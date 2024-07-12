using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface ITransactionRepository
    {
        public List<TransactionHistory> GetAllTransactions(int pageNumber, int pageSize, string sellerName, string buyerName);

        public int CountTransactions(string sellerName, string buyerName);

        public void AddToTransaction(TransactionHistory transaction);
    }
}

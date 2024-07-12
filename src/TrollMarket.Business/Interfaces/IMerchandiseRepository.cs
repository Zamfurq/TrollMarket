using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface IMerchandiseRepository
    {
        public List<Merchandise> GetProductBySeller(int pageNumber, int pageSize, string username);

        public List<Merchandise> GetAllProduct(int pageNumber, int pageSize, string username, string productName, string category);

        public int CountProductBySeller(string username);

        public int CountAllProduct(string username, string productName, string category);

        public Merchandise? GetProductById(int? id);

        public void Insert(Merchandise merchandise);

        public void Update(Merchandise merchandise);

        public void Delete(Merchandise merchandise);

        public List<Cart>? GetCartByMerchandiseId(int merchandiseId);

        public List<TransactionHistory>? GetTransactionByMerchandiseId(int merchandiseId);

        public string GetFullName(string username); 
    }
}

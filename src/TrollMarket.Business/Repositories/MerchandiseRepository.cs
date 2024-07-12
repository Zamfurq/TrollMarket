using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Repositories
{
    public class MerchandiseRepository : IMerchandiseRepository
    {
        private readonly TrollMarketContext _dbContext;

        public MerchandiseRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Merchandise> GetProductBySeller(int pageNumber, int pageSize, string username)
        {
            var query = from merchandise in _dbContext.Merchandises
                        where merchandise.SellerName.Equals(username)
                        select merchandise;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountProductBySeller(string username)
        {
            var query = from merchandise in _dbContext.Merchandises
                        where merchandise.SellerName.Equals(username)
                        select merchandise;

            return query.Count();
        }

        public List<Merchandise> GetAllProduct(int pageNumber, int pageSize, string username, string productName, string category)
        {
            var query = from merchandise in _dbContext.Merchandises
                        where merchandise.SellerName != username && merchandise.IsDiscontinued == false
                        && (productName == null || merchandise.ProductName.Contains(productName))
                        && (category == null || merchandise.Category.Contains(category))
                        select merchandise;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountAllProduct(string username, string productName, string category)
        {
            var query = from merchandise in _dbContext.Merchandises
                        where merchandise.SellerName != username && merchandise.IsDiscontinued == false
                        && (productName == null || merchandise.ProductName.Contains(productName))
                        && (category == null || merchandise.Category.Contains(category))
                        select merchandise;

            return query.Count();
        }

        public void Insert(Merchandise merchandise)
        {
            _dbContext.Merchandises.Add(merchandise);
            _dbContext.SaveChanges();
        }

        public Merchandise? GetProductById(int? id)
        {
            return _dbContext.Merchandises.FirstOrDefault(m => m.MerchandiseId.Equals(id))
                ?? null;
        }

        public void Update(Merchandise merchandise)
        {
            if (merchandise.MerchandiseId == 0)
            {
                throw new Exception("Product did not exist");
            }
            _dbContext.Update(merchandise);
            _dbContext.SaveChanges();
        }

        public void Delete(Merchandise merchandise)
        {
            _dbContext.Merchandises.Remove(merchandise);
            _dbContext.SaveChanges();
        }

        public string GetFullName(string username)
        {
            return _dbContext.UserDetails.FirstOrDefault(u => u.Username.Equals(username)).FirstName + " "
                + _dbContext.UserDetails.FirstOrDefault(u => u.Username.Equals(username)).LastName
                ?? throw new Exception("Username not found");
        }

        public List<Cart>? GetCartByMerchandiseId(int shipmentId)
        {
            return _dbContext.Carts.Where(c => c.MerchaniseId.Equals(shipmentId)).ToList()
                ?? null;
        }

        public List<TransactionHistory>? GetTransactionByMerchandiseId(int shipmentId)
        {
            return _dbContext.TransactionHistories.Where(c => c.MerchandiseId.Equals(shipmentId)).ToList()
                ?? null;
        }
    }
}

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
    public class CartRepository : ICartRepository
    {
        private readonly TrollMarketContext _dbContext;

        public CartRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CountCart(string username)
        {
            var query = from cart in _dbContext.Carts
                        where cart.BuyerName.Equals(username)
                        select cart;

            return query.Count();
        }

        public List<Cart> GetAllCart(int pageNumber, int pageSize, string username)
        {
            var query = from cart in _dbContext.Carts
                        where cart.BuyerName.Equals(username)
                        select cart;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public Cart? GetCart(string username, int productId)
        {
            return _dbContext.Carts.FirstOrDefault(c => c.MerchaniseId.Equals(productId) && c.BuyerName.Equals(username)) ??
                null;
        }

        public void Insert(Cart cart)
        {
            _dbContext.Carts.Add(cart);
            _dbContext.SaveChanges();
        }

        public void Update(Cart cart)
        {
            _dbContext.Carts.Update(cart);
            _dbContext.SaveChanges();
        }

        public void Delete(Cart cart)
        {
            _dbContext.Carts.Remove(cart);
            _dbContext.SaveChanges();   
        }

        public void PurchaseAll(List<Cart> carts) {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach (var cart in carts)
                    {
                        decimal sellerGain = cart.Quantity * cart.Merchanise.Price;
                        TransactionHistory transactionHistory = new TransactionHistory
                        {
                            BuyerName = cart.BuyerName,
                            SellerName = cart.Merchanise.SellerName,
                            Quantity = cart.Quantity,
                            ShipmentId = cart.ShipmentId,
                            MerchandiseId = cart.MerchaniseId,
                            ShipmentPrice = cart.Shipment.Price,
                            UnitPrice = cart.Merchanise.Price,
                            TransactionDate = DateTime.Now
                        };
                        UserDetail seller = _dbContext.UserDetails.FirstOrDefault(u => u.Username.Equals(cart.Merchanise.SellerName));
                        seller.Balance += sellerGain;
                        UserDetail buyer = _dbContext.UserDetails.FirstOrDefault(u => u.Username.Equals(cart.BuyerName));
                        buyer.Balance -= (cart.Quantity * cart.Merchanise.Price) + cart.Shipment.Price;
                        _dbContext.UserDetails.Update(seller);
                        _dbContext.UserDetails.Update(buyer);
                        _dbContext.TransactionHistories.Add(transactionHistory);
                        _dbContext.Carts.Remove(cart);
                        _dbContext.SaveChanges();
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                }
            }
            
            
            
        }
    }
}

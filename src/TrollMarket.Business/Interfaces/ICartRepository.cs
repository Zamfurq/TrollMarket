using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface ICartRepository
    {
        public List<Cart> GetAllCart(int pageNumber, int pageSize, string username);

        public int CountCart(string username);

        public Cart? GetCart(string username, int productId);

        public void Insert(Cart cart);

        public void Update(Cart cart);

        public void Delete(Cart cart);

        public void PurchaseAll(List<Cart> carts);
    }
}

using TrollMarket.Business.Interfaces;
using TrollMarket.Business.Repositories;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Services
{
    public class CartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IMerchandiseRepository _merchandiseRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public CartService(ICartRepository cartRepository, IShipmentRepository shipmentRepository,
            IMerchandiseRepository merchandiseRepository, IAccountRepository accountRepository,
            ITransactionRepository transactionRepository)
        {
            _cartRepository = cartRepository;
            _shipmentRepository = shipmentRepository;
            _merchandiseRepository = merchandiseRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        public CartindexViewModel GetAllCarts(int pageNumber, int pageSize, string username)
        {
            List<CartViewModel> carts = _cartRepository.GetAllCart(pageNumber,pageSize,username)
                .Select(c => new CartViewModel
                {
                    BuyerName = c.BuyerName,
                    MerchaniseId = c.MerchaniseId,
                    ShipmentId = c.ShipmentId,
                    Quantity = c.Quantity,
                    Shipment = _shipmentRepository.GetShipment(c.ShipmentId),
                    Merchandise = _merchandiseRepository.GetProductById(c.MerchaniseId),
                    SellerName = _merchandiseRepository.GetFullName(c.Merchanise.SellerName)
                }).ToList();

            int totalItem = _cartRepository.CountCart(username);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new CartindexViewModel
            {
                Carts = carts,
                PageNumber = pageNumber,
                TotalPage = pageTotal,
            };
        }

        public decimal GetCalculation(CartindexViewModel vm, string username)
        {
            UserDetail users = _accountRepository.GetUserDetail(username);
            decimal balance = users.Balance;
            foreach (var cart in vm.Carts)
            {
                Merchandise merchandise = _merchandiseRepository.GetProductById(cart.MerchaniseId);
                Shipment shipment = _shipmentRepository.GetShipment(cart.ShipmentId);
                balance -= (cart.Quantity * merchandise.Price) + shipment.Price;
            }
            return balance;
        }

        public void PurchaseAll(CartindexViewModel vm, string username) {
            List<Cart> carts = new List<Cart>();
            foreach (var cart in vm.Carts)
            {
                Cart theCart = new Cart { 
                    BuyerName = username,
                    MerchaniseId = cart.MerchaniseId,
                    ShipmentId = cart.ShipmentId,
                    Quantity = cart.Quantity,
                    Merchanise = _merchandiseRepository.GetProductById(cart.MerchaniseId),
                    Shipment = _shipmentRepository.GetShipment(cart.ShipmentId)
                };
                carts.Add(theCart);
            }
            _cartRepository.PurchaseAll(carts);
        }

        public void Delete(int merchId, string buyerName)
        {
            Cart theCart = _cartRepository.GetCart(buyerName, merchId);
            _cartRepository.Delete(theCart);
        }
    }
}

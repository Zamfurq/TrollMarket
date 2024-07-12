using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Services
{
    public class ShopService
    {
        private readonly IMerchandiseRepository _merchandiseRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly ICartRepository _cartRepository;

        public ShopService(IMerchandiseRepository merchandiseRepository, 
            IShipmentRepository shipmentRepository,
            ICartRepository cartRepository)
        {
            _merchandiseRepository = merchandiseRepository;
            _shipmentRepository = shipmentRepository;
            _cartRepository = cartRepository;
        }

        public MerchIndexViewModel GetAvailableProduct(int pageNumber, int pageSize, string username, string productName, string category)
        {
            List<MerchViewModel> merchs = _merchandiseRepository.GetAllProduct(pageNumber, pageSize, username, productName, category)
                .Select(c => new MerchViewModel
                {
                    MerchandiseId = c.MerchandiseId,
                    SellerName = c.SellerName,
                    Category = c.Category,
                    ProductName = c.ProductName,
                    Price = c.Price,
                    Description = c.Description,
                    IsDiscontinued = c.IsDiscontinued,
                    FullName = _merchandiseRepository.GetFullName(c.SellerName)
                }).ToList();

            int totalItem = _merchandiseRepository.CountAllProduct(username, productName, category);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new MerchIndexViewModel
            {
                Merch = merchs,
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Name = productName,
                Category = category,
                Shipments = GetShipment()
            };
        }

        public void AddtoCart(CartViewModel cart)
        {
            Cart theCart = _cartRepository.GetCart(cart.BuyerName,cart.MerchaniseId);
            if (theCart == null)
            {
                Cart newCart = new Cart
                {
                    BuyerName = cart.BuyerName,
                    ShipmentId = cart.ShipmentId,
                    MerchaniseId = cart.MerchaniseId,
                    Quantity = cart.Quantity
                };
                _cartRepository.Insert(newCart);
            } else
            {
                theCart.Quantity += cart.Quantity;
                theCart.ShipmentId = cart.ShipmentId;
                _cartRepository.Update(theCart);
            }
            
        }

        public List<SelectListItem> GetShipment()
        {
            var shipment = _shipmentRepository.GetShipments();

            var selectListItems = shipment.OrderBy(s => s.ShipperName)
                .Select(s => new SelectListItem { Value = s.ShipmentId.ToString(), Text = s.ShipperName})
                .ToList();

            return selectListItems;
        }
    }
}

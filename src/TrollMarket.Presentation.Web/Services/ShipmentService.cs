using TrollMarket.Business.Interfaces;
using TrollMarket.Business.Repositories;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Services
{
    public class ShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;

        public ShipmentService(IShipmentRepository shipmentRepository)
        {
            _shipmentRepository = shipmentRepository;
        }

        public ShipmentIndexViewModel GetShipments(int pageNumber, int pageSize)
        {
            List<ShipmentViewModel> shipments = _shipmentRepository.GetAllShipments(pageNumber, pageSize)
                .Select(c => new ShipmentViewModel
                {
                    ShipmentId = c.ShipmentId,
                    ShipperName = c.ShipperName,
                    Price = c.Price,
                    IsService = c.IsService,
                }).ToList();

            int totalItem = _shipmentRepository.CountShipments();
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new ShipmentIndexViewModel
            {
                Shipments = shipments,
                PageNumber = pageNumber,
                TotalPage = pageTotal
            };
        }

        public void InsertShipment(ShipmentViewModel shipment)
        {
            Shipment newShipment = new Shipment
            {
                ShipperName = shipment.ShipperName,
                Price = shipment.Price,
                IsService = shipment.IsService,
            };
            _shipmentRepository.Insert(newShipment);
        }

        public void UpdateShipment(int id,ShipmentViewModel shipment)
        {
            Shipment theShipment = _shipmentRepository.GetShipment(id);
            theShipment.ShipperName = shipment.ShipperName;
            theShipment.IsService = shipment.IsService;
            theShipment.Price = shipment.Price;
            _shipmentRepository.Update(theShipment);
        }

        public ShipmentViewModel GetShipmentByID(int id)
        {
            Shipment shipment = _shipmentRepository.GetShipment(id);
            return new ShipmentViewModel
            {
                ShipmentId = shipment.ShipmentId,
                ShipperName = shipment.ShipperName,
                IsService = shipment.IsService,
                Price = shipment.Price
            };
        }

        public List<Cart>? GetCart(int id)
        {
            List<Cart>? cart = _shipmentRepository.GetCartByShipmentId(id);
            return cart;
        }

        public List<TransactionHistory>? GetTransactions(int id)
        {
            List<TransactionHistory>? transactions = _shipmentRepository.GetTransactionByShipmentId(id);
            return transactions;
        }

        public void StopShipment(int id)
        {
            Shipment shipment = _shipmentRepository.GetShipment(id);
            shipment.IsService = false;
            _shipmentRepository.Update(shipment);
        }

        public void DeleteShipment(int id)
        {
            Shipment shipment = _shipmentRepository.GetShipment(id);
            _shipmentRepository.Delete(shipment);
        }
    }
}

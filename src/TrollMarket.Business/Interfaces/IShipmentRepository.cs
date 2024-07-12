using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Interfaces
{
    public interface IShipmentRepository
    {
        public List<Shipment> GetAllShipments(int pageNumber, int pageSize);

        public int CountShipments();

        public Shipment? GetShipment(int shipmentId);

        public void Insert(Shipment shipment);

        public void Update(Shipment shipment);

        public void Delete(Shipment shipment);

        public List<Cart>? GetCartByShipmentId(int shipmentId);

        public List<TransactionHistory>? GetTransactionByShipmentId(int shipmentId);

        public List<Shipment> GetShipments();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrollMarket.Business.Interfaces;
using TrollMarket.DataAccess.Models;

namespace TrollMarket.Business.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly TrollMarketContext _dbContext;

        public ShipmentRepository(TrollMarketContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Shipment> GetAllShipments(int pageNumber, int pageSize)
        {
            var query = from shipment in _dbContext.Shipments
                        select shipment;

            return query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        }

        public int CountShipments()
        {
            var query = from shipment in _dbContext.Shipments
                        select shipment;

            return query.Count();
        }

        public Shipment? GetShipment(int shipmentId)
        {
            return _dbContext.Shipments.FirstOrDefault(s => s.ShipmentId.Equals(shipmentId))
                ?? null;
        }

        public void Insert(Shipment shipment)
        {
            _dbContext.Shipments.Add(shipment);
            _dbContext.SaveChanges();
        }

        public void Update(Shipment shipment)
        {
            if (shipment.ShipmentId == 0) {
                throw new Exception("Shipment did not exist");
            }
            _dbContext.Shipments.Update(shipment);
            _dbContext.SaveChanges();
        }

        public void Delete(Shipment shipment)
        {
            _dbContext.Shipments.Remove(shipment);
            _dbContext.SaveChanges();
        }

        public List<Shipment> GetShipments()
        {
            return _dbContext.Shipments.Where(s => s.IsService == true).ToList();
        }

        public List<Cart>? GetCartByShipmentId(int shipmentId)
        {
            return _dbContext.Carts.Where(c => c.ShipmentId.Equals(shipmentId)).ToList()
                ?? null;
        }

        public List<TransactionHistory>? GetTransactionByShipmentId(int shipmentId)
        {
            return _dbContext.TransactionHistories.Where(c => c.ShipmentId.Equals(shipmentId)).ToList()
                ?? null;
        }
    }
}

using TrollMarket.Business.Interfaces;
using TrollMarket.Business.Repositories;
using TrollMarket.DataAccess.Models.Enum;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Services
{
    public class ProfileService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMerchandiseRepository _merchandiseRepository;
        private readonly IShipmentRepository _shipmentRepository;

        public ProfileService(IAccountRepository accountRepository,IShipmentRepository shipmentRepository,
            IMerchandiseRepository merchandiseRepository)
        {
            _accountRepository = accountRepository;
            _shipmentRepository = shipmentRepository;
            _merchandiseRepository = merchandiseRepository;
        }

        public ProfileViewModel GetProfile(string username)
        {
            var profile = _accountRepository.GetUserDetail(username);

            ProfileViewModel vm = new ProfileViewModel
            {
                Username = profile.Username,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Address = profile.Address,
                Balance = profile.Balance
            };
            return vm;
        }

        public TransactionIndexViewModel GetTransactionIndex(string username, string role, int pageNumber, int pageSize) {
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();
            int totalItem = 0;
            if (role == "Seller")
            {
                transactions = _accountRepository.GetTransactionsBySeller(pageNumber,pageSize,username)
                .Select(t => new TransactionViewModel
                {
                    BuyerName = _merchandiseRepository.GetFullName(t.BuyerName),
                    SellerName = _merchandiseRepository.GetFullName(t.SellerName),
                    TransactionDate = t.TransactionDate,
                    Price = t.UnitPrice,
                    Shipment = _shipmentRepository.GetShipment(t.ShipmentId),
                    ShipmentPrice = t.ShipmentPrice,
                    Quantity = t.Quantity,
                    TransactionId = t.TransactionId,
                    Merchandise = _merchandiseRepository.GetProductById(t.MerchandiseId)
                }).ToList();
                totalItem = _accountRepository.CountTransactionBySeller(username);
            } else if (role == "Buyer") {
                transactions = _accountRepository.GetTransactionsByBuyer(pageNumber, pageSize, username)
                .Select(t => new TransactionViewModel
                {
                    BuyerName = _merchandiseRepository.GetFullName(t.BuyerName),
                    SellerName = _merchandiseRepository.GetFullName(t.SellerName),
                    TransactionDate = t.TransactionDate,
                    Price = t.UnitPrice,
                    Shipment = _shipmentRepository.GetShipment(t.ShipmentId),
                    ShipmentPrice = t.ShipmentPrice,
                    Quantity = t.Quantity,
                    TransactionId = t.TransactionId,
                    Merchandise = _merchandiseRepository.GetProductById(t.MerchandiseId)
                }).ToList();
                totalItem = _accountRepository.CountTransactionByBuyer(username);
            }

            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new TransactionIndexViewModel
            {
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Transactions = transactions,
            };
        }

        public void AddBalance(string username, decimal balance) {
            var profile = _accountRepository.GetUserDetail(username);
            profile.Balance += balance;
            _accountRepository.UpdateUserDetail(profile);
        }
    }
}

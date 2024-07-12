using Microsoft.AspNetCore.Mvc.Rendering;
using TrollMarket.Business.Interfaces;
using TrollMarket.Business.Repositories;
using TrollMarket.DataAccess.Models.Enum;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Services
{
    public class TransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IMerchandiseRepository _merchandiseRepository;
        private readonly IShipmentRepository _shipmentRepository;

        public TransactionService(ITransactionRepository transactionRepository, IShipmentRepository shipmentRepository,
            IAccountRepository accountRepository, IMerchandiseRepository merchandiseRepository)
        {
            _transactionRepository = transactionRepository;
            _shipmentRepository = shipmentRepository;
            _accountRepository = accountRepository;
            _merchandiseRepository = merchandiseRepository;
        }

        public TransactionIndexViewModel GetAllTransaction(int pageNumber, int pageSize, string sellerName, string buyerName)
        {
            List<TransactionViewModel> transactions = _transactionRepository.GetAllTransactions(pageNumber, pageSize, sellerName, buyerName)
                .Select(t =>  new TransactionViewModel
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

            int totalItem = _transactionRepository.CountTransactions(sellerName, buyerName);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new TransactionIndexViewModel
            {
                PageNumber = pageNumber,
                TotalPage = pageTotal,
                Transactions = transactions,
                Usernames = GetUsernames(),
            };
        }

        public List<SelectListItem> GetUsernames()
        {
            var accounts = _accountRepository.GetAllAccounts();

            List<SelectListItem> result = new List<SelectListItem>();

            foreach (var account in accounts)
            {
                result.Add(new SelectListItem
                {
                    Text = account.Username,
                    Value = account.Username
                });
            }

            return result;
        }
    }
}

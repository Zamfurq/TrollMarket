using TrollMarket.Business.Interfaces;
using TrollMarket.Business.Repositories;
using TrollMarket.DataAccess.Models;
using TrollMarket.Presentation.Web.Models;

namespace TrollMarket.Presentation.Web.Services
{
    public class MerchandiseService
    {
        private readonly IMerchandiseRepository _merchandiseRepository;

        public MerchandiseService(IMerchandiseRepository merchandiseRepository)
        {
            _merchandiseRepository = merchandiseRepository;
        }

        public MerchIndexViewModel GetAllProduct(int pageNumber, int pageSize, string username)
        {
            List<MerchViewModel> merchs = _merchandiseRepository.GetProductBySeller(pageNumber, pageSize, username)
                .Select(c => new MerchViewModel
                {
                    MerchandiseId = c.MerchandiseId,
                    SellerName = c.SellerName,
                    Category = c.Category,
                    ProductName = c.ProductName,
                    Price   = c.Price,
                    Description = c.Description,
                    IsDiscontinued  = c.IsDiscontinued
                }).ToList();

            int totalItem = _merchandiseRepository.CountProductBySeller(username);
            int pageTotal = (int)Math.Ceiling((decimal)totalItem / (decimal)pageSize);

            return new MerchIndexViewModel
            {
                Merch = merchs,
                PageNumber = pageNumber,
                TotalPage = pageTotal
            };
        }

        public MerchViewModel GetMerchById(int id)
        {
            Merchandise merchandise = _merchandiseRepository.GetProductById(id);
            return new MerchViewModel
            {
                Category = merchandise.Category,
                Description = merchandise.Description,
                ProductName = merchandise.ProductName,
                IsDiscontinued = merchandise.IsDiscontinued,
                MerchandiseId = merchandise.MerchandiseId,
                SellerName = merchandise.SellerName,
                Price = merchandise.Price,
                FullName = _merchandiseRepository.GetFullName(merchandise.SellerName)
            };
        }

        public void Insert(MerchViewModel vm)
        {
            Merchandise newMerchandise = new Merchandise
            {
                ProductName = vm.ProductName,
                Price = vm.Price,
                Category = vm.Category,
                Description = vm.Description,
                IsDiscontinued = vm.IsDiscontinued,
                SellerName = vm.SellerName
            };
            _merchandiseRepository.Insert(newMerchandise);
        }

        public void Update(MerchViewModel vm)
        {
            Merchandise merchandise = _merchandiseRepository.GetProductById(vm.MerchandiseId);
            merchandise.Price = vm.Price;
            merchandise.Category = vm.Category;
            merchandise.ProductName = vm.ProductName;
            merchandise.IsDiscontinued = vm.IsDiscontinued;
            merchandise.Description = vm.Description;
            _merchandiseRepository.Update(merchandise);
        }

        public List<Cart>? GetCart(int id)
        {
            List<Cart>? cart = _merchandiseRepository.GetCartByMerchandiseId(id);
            return cart;
        }

        public List<TransactionHistory>? GetTransactions(int id)
        {
            List<TransactionHistory>? transactions = _merchandiseRepository.GetTransactionByMerchandiseId(id);
            return transactions;
        }

        public void Discontinue(int id)
        {
            Merchandise merchandise = _merchandiseRepository.GetProductById(id);
            merchandise.IsDiscontinued = true;
            _merchandiseRepository.Update(merchandise);
        }

        public void Delete(int id)
        {
            Merchandise merchandise = _merchandiseRepository.GetProductById(id);
            _merchandiseRepository.Delete(merchandise);
        }

    }
}

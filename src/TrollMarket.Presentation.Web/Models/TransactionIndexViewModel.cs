using Microsoft.AspNetCore.Mvc.Rendering;

namespace TrollMarket.Presentation.Web.Models
{
    public class TransactionIndexViewModel
    {
        public int PageNumber { get; set; }

        public int TotalPage { get; set; }
        public List<TransactionViewModel> Transactions { get; set; }

        public string SellerName { get; set; }

        public string BuyerName { get; set; }

        public List<SelectListItem>? Usernames { get; set; }
    }
}

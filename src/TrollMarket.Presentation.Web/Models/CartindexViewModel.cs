namespace TrollMarket.Presentation.Web.Models
{
    public class CartindexViewModel
    {
        public int PageNumber { get; set; }

        public int TotalPage { get; set; }

        public List<CartViewModel> Carts { get; set; }
    }
}

namespace TrollMarket.Presentation.Web.Models
{
    public class ShipmentIndexViewModel
    {
        public int PageNumber { get; set; }

        public int TotalPage { get; set; }
        public List<ShipmentViewModel> Shipments { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;

namespace TrollMarket.Presentation.Web.Models
{
    public class MerchIndexViewModel
    {
        public int PageNumber { get; set; }

        public DataTable Merchs { get; set; }   

        public int TotalPage { get; set; }

        public string? Name { get; set; }

        public string? Category { get; set; }
        public List<MerchViewModel> Merch { get; set; }

        public List<SelectListItem>? Shipments { get; set; } = null;
    }
}

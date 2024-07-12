namespace TrollMarket.Presentation.Web.Models
{
    public class ProfileDetailViewModel
    {
        public ProfileViewModel Profile { get; set; } = new ProfileViewModel();

        public TransactionIndexViewModel TransactionIndex { get; set; } = new TransactionIndexViewModel();  
    }
}

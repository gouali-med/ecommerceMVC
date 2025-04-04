namespace EcommerceMVC.Web.Models
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; }
        public List<string> Categories { get; set; }
        public List<Product> LatestProducts { get; set; }
        public string SearchString { get; set; }
        public string SelectedCategory { get; set; }
        public string SortBy { get; set; }
    }
} 
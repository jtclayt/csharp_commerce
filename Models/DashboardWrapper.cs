namespace Commerce.Models
{
    public class DashboardWrapper
    {
        public Product[] NewestProducts { get; set; }

        public Order[] RecentOrder { get; set; }

        public User[] NewestUsers { get; set; }
    }
}

namespace eCommerce.Models
{
    public class Company_Product
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}

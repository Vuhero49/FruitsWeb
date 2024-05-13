using eCommerce.Data.Base;
using eCommerce.Data.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerce.Models
{
    public class Product : IEntityBase
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
		public string Description { get; set; }
		public double Price { get; set; }
		public string ImageURL { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public ProductCategory ProductCategory { get; set; }

        // Relationships
        public List<Company_Product> Company_Products { get; set; }

        // Store
        public int StoreId { get; set; }
        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        // City
        public int CityId { get; set; }
        [ForeignKey("CityId")]
        public City City { get; set; }

    }
}

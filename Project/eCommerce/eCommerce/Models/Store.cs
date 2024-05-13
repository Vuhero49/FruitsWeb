using eCommerce.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class Store : IEntityBase
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Store Logo")]
        [Required(ErrorMessage = "Store Logo is required")]
        public string Logo { get; set; }

		[Display(Name = "Store Name")]
        [Required(ErrorMessage = "Store Name is required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name must be between 3 and 50 chars")]
        public string Name { get; set; }

		[Display(Name = "Store Description")]
        [Required(ErrorMessage = "Store Description is required")]
        public string Description { get; set; }

        // Relationships
        public List<Product> Products { get; set; }
    }
}

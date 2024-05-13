using eCommerce.Data.Base;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Models
{
    public class Company : IEntityBase
    {
        [Key]

        public int Id { get; set; }

        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage ="Profile Picture is required")]
        public  string ProfilePicture { get; set; }

		[Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Full Name must between 10 and 50 chars")]
        public  string FullName { get; set; }

		[Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography is required")]
        public string Bio { get; set; }

        //Relationships
        public List<Company_Product> Company_Products { get; set; }
    }
}

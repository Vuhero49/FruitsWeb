using eCommerce.Data.Enum;
using System.ComponentModel.DataAnnotations;

namespace eCommerce.Data.ViewModels
{
    public class NewProductVM
    {
        public int Id { get; set; }

        [Display(Name = "Product name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Display(Name = "Product description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Price in $")]
        [Required(ErrorMessage = "Price is required")]
        public double Price { get; set; }

        [Display(Name = "Product poster URL")]
        [Required(ErrorMessage = "Product poster URL is required")]
        public string ImageURL { get; set; }

        [Display(Name = "Product start date")]
        [Required(ErrorMessage = "Start date is required")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Product end date")]
        [Required(ErrorMessage = "End date is required")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Select a category")]
        [Required(ErrorMessage = "Product category is required")]
        public ProductCategory ProductCategory { get; set; }

        //Relationships
        [Display(Name = "Select Company(s)")]
        [Required(ErrorMessage = "Product company(s) is required")]
        public List<int> CompanyIds { get; set; }

        [Display(Name = "Select a Store")]
        [Required(ErrorMessage = "Product store is required")]
        public int StoreId { get; set; }

        [Display(Name = "Select a City")]
        [Required(ErrorMessage = "Product city is required")]
        public int CityId { get; set; }
    }
}

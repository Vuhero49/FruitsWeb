using System.ComponentModel.DataAnnotations;

namespace eCommerce.Data.ViewModels
{
    public class RegisterVM
    {
        [Display(Name = "Full name")]
        [Required(ErrorMessage = "Full name is required")]
        public string FullName { get; set; }

        [Display(Name = "Email address")]
        [Required(ErrorMessage = "Email address is required")]
        public string EmailAddress { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "User role is required")]
        public string UserRoles { get; set; }

        public RegisterVM()
        {
            // Đặt giá trị mặc định cho UserRoles nếu không có giá trị được cung cấp
            UserRoles = "user";
        }
    }
}

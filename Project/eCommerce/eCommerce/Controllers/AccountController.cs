using eCommerce.Data.Static;
using eCommerce.Data.ViewModels;
using eCommerce.Data;
using eCommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eCommerce.Data.Cart;

namespace eCommerce.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AppDbContext _context;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        // List of all user
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }


        public IActionResult Login() => View(new LoginVM());

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (user != null)
            {
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        var shoppingCart = ShoppingCart.GetShoppingCart(HttpContext.RequestServices);

                        // Clear the shopping cart after successful login
                        await shoppingCart.ClearShoppingCartAsync();
                        return RedirectToAction("Index", "Products");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please, try again!";
                return View(loginVM);
            }

            TempData["Error"] = "Wrong credentials. Please, try again!";
            return View(loginVM);
        }


        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerVM);
            }

            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress,
                UserRoles = "user"
            };

            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                // Gán vai trò mặc định
                await _userManager.AddToRoleAsync(newUser, string.IsNullOrEmpty(registerVM.UserRoles) ? "user" : registerVM.UserRoles);

                await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu

                //await _signInManager.SignInAsync(newUser, isPersistent: false); // Đăng nhập người dùng mới
                //return RedirectToAction("Index", "Products");

                return View("RegisterCompleted"); // Có thể bỏ 2 dòng trên để tiến tới trang xác nhận
            }
            else
            {
                foreach (var error in newUserResponse.Errors)
                {
                    Console.WriteLine($"Error: {error.Description}");
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(registerVM);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Products");
        }

        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}

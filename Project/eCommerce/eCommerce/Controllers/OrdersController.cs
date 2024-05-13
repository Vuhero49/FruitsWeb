using eCommerce.Data.Cart;
using eCommerce.Data.Services;
using eCommerce.Data.ViewModels;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eCommerce.Controllers
{
    
    public class OrdersController : Controller
    {
        private readonly IProductsService _productService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrdersService _ordersService;

        public OrdersController(IProductsService productService, ShoppingCart shoppingCart, IOrdersService ordersService)
        {
            _productService = productService;
            _shoppingCart = shoppingCart;
            _ordersService = ordersService;
        }

        public async Task<IActionResult> Index()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy UserId của người dùng hiện tại
            string userRole = User.FindFirstValue(ClaimTypes.Role);

            // Sử dụng dịch vụ hoặc truy vấn để lấy danh sách đơn hàng của người dùng
            var orders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);

            return View(orders);
        }

        public IActionResult ShoppingCart()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            _shoppingCart.ShoppingCartItems = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(response);
        }

        [AllowAnonymous]
        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _productService.GetProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        [AllowAnonymous]
        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _productService.GetProductByIdAsync(id);

            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        [Authorize]
        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItems();
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Lấy UserId của người dùng hiện tại
            string userEmailAddress = User.FindFirstValue(ClaimTypes.Email); // Lấy địa chỉ email của người dùng hiện tại

            // Tạo đơn hàng với thông tin của người dùng hiện tại
            var order = new Order
            {
                Email = userEmailAddress,
                UserId = userId,
                OrderItems = items.Select(item => new OrderItem
                {
                    Amount = item.Amount,
                    Price = item.Product.Price,
                    ProductId = item.Product.Id
                }).ToList()
            };

            // Lưu đơn hàng vào Cơ sở dữ liệu    
            await _ordersService.AddOrderAsync(order);

            // Xóa giỏ hàng sau khi thanh toán
            await _shoppingCart.ClearShoppingCartAsync();

            return View("OrderCompleted");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _ordersService.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            await _ordersService.DeleteAsync(id);

            // Cập nhật lại danh sách đơn hàng sau khi xóa
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            string userRole = User.FindFirstValue(ClaimTypes.Role);
            var updatedOrders = await _ordersService.GetOrdersByUserIdAndRoleAsync(userId, userRole);

            return View("Index", updatedOrders);
        }

    }
}

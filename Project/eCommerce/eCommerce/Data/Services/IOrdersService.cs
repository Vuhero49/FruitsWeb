using eCommerce.Models;

namespace eCommerce.Data.Services
{
    public interface IOrdersService 
    {
        Task AddOrderAsync(Order order);
        Task<Order> GetOrderByIdAsync(int id);
        public Task DeleteAsync(int id);
        Task DeleteOrderAsync(Order order);
        Task StoreOrderAsync(List<ShoppingCartItem> items, string userId, string userEmailAddress);
        Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole);

    }
}

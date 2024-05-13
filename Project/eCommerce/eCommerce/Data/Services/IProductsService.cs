using eCommerce.Data.Base;
using eCommerce.Data.Enum;
using eCommerce.Data.ViewModels;
using eCommerce.Models;

namespace eCommerce.Data.Services
{
    public interface IProductsService : IEntityBaseReponsitory<Product>
    {
        Task <Product> GetProductByIdAsync(int id);
        Task <NewProductsDropdownsVM> GetNewProductDropdownsValues();
        Task AddNewProductAsync(NewProductVM data);
        Task UpdateProductAsync(NewProductVM data);
        Task DeleteProductAsync(int id);
        Task<IEnumerable<ProductCategory>> GetDistinctCategoriesAsync(); // Change the return type to ProductCategory enum
        Task<IEnumerable<Product>> GetProductsByCategoryAsync(ProductCategory category); // Change the parameter type to ProductCategory enum

    }
}

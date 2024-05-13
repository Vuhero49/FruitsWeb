using eCommerce.Data.Base;
using eCommerce.Data.Enum;
using eCommerce.Data.ViewModels;
using eCommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Data.Services
{
    public class ProductsService : EntityBaseReponsitory<Product>, IProductsService
    {
        private readonly AppDbContext _context;
        public ProductsService(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task AddNewProductAsync(NewProductVM data)
        {
            var newProduct = new Product()
            {
                Name = data.Name,
                Description = data.Description,
                Price = data.Price,
                ImageURL = data.ImageURL,
                StoreId = data.StoreId,
                StartDay = data.StartDate,
                EndDay = data.EndDate,
                ProductCategory = data.ProductCategory,
                CityId = data.CityId
            };
            await _context.Products.AddAsync(newProduct);
            await _context.SaveChangesAsync();

            //Add Product Company
            foreach (var companyId in data.CompanyIds)
            {
                var newCompanyProduct = new Company_Product()
                {
                    ProductId = newProduct.Id,
                    CompanyId = companyId
                };
                await _context.Company_Products.AddAsync(newCompanyProduct);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            var productDetails = await _context.Products
                .Include(c => c.Store)
                .Include(p => p.City)
                .Include(am => am.Company_Products).ThenInclude(a => a.Company)
                .FirstOrDefaultAsync(n => n.Id == id);

            return productDetails;
        }

        public async Task<NewProductsDropdownsVM> GetNewProductDropdownsValues()
        {
            var response = new NewProductsDropdownsVM()
            {
                Companies = await _context.Companies.OrderBy(n => n.FullName).ToListAsync(),
                Stores = await _context.Stores.OrderBy(n => n.Name).ToListAsync(),
                Cities = await _context.Cities.OrderBy(n => n.FullName).ToListAsync()
            };

            return response;
        }

        public async Task UpdateProductAsync(NewProductVM data)
        {
            var dbProduct = await _context.Products.FirstOrDefaultAsync(n => n.Id == data.Id);

            if (dbProduct != null)
            {
                dbProduct.Name = data.Name;
                dbProduct.Description = data.Description;
                dbProduct.Price = data.Price;
                dbProduct.ImageURL = data.ImageURL;
                dbProduct.StoreId = data.StoreId;
                dbProduct.StartDay = data.StartDate;
                dbProduct.EndDay = data.EndDate;
                dbProduct.ProductCategory = data.ProductCategory;
                dbProduct.CityId = data.CityId;
                await _context.SaveChangesAsync();
            }

            //Remove existing companies
            var existingCompaniesDb = _context.Company_Products.Where(n => n.ProductId == data.Id).ToList();
            _context.Company_Products.RemoveRange(existingCompaniesDb);
            await _context.SaveChangesAsync();

            //Add Company Product
            foreach (var companyId in data.CompanyIds)
            {
                var newCompanyProduct = new Company_Product()
                {
                    ProductId = data.Id,
                    CompanyId = companyId
                };
                await _context.Company_Products.AddAsync(newCompanyProduct);
            }
            await _context.SaveChangesAsync();
        }
        
        //DELETE
        public async Task DeleteProductAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductCategory>> GetDistinctCategoriesAsync()
        {
            return await _context.Products
                .Select(p => p.ProductCategory)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(ProductCategory category)
        {
            return await _context.Products
                .Where(p => p.ProductCategory == category)
                .ToListAsync();
        }
    }
}

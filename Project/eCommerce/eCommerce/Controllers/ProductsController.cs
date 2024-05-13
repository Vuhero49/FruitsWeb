using eCommerce.Data;
using eCommerce.Data.Enum;
using eCommerce.Data.Services;
using eCommerce.Data.Static;
using eCommerce.Data.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductsController : Controller
    {

        private readonly IProductsService _service;

        public ProductsController(IProductsService service)
        {
            _service = service;
        }

        [AllowAnonymous] //chỉ định 1 action hoặc controller có thể truy cập mà không cần xác thực (đăng nhập)
        public async Task<IActionResult> Index(ProductCategory? category)
        {
            var allProducts = !category.HasValue
            ? await _service.GetAllAsync(n => n.Store)
            : await _service.GetProductsByCategoryAsync(category.Value);

            ViewBag.SelectedCategory = category; // Pass the selected category to the view
            ViewBag.Categories = await _service.GetDistinctCategoriesAsync(); // For dropdown

            return View(allProducts);
        }
        [AllowAnonymous]
        public async Task<IActionResult> Filter(string searchString)
        {
            var allProducts = await _service.GetAllAsync();

            if (!string.IsNullOrEmpty(searchString))
            {
                var filteredResult = allProducts
                    .Where(n => n.Name.StartsWith(searchString, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();

                return View("Index", filteredResult);
            }

            return View("Index", allProducts);
        }
        //GET: Products/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var movieDetail = await _service.GetProductByIdAsync(id);
            return View(movieDetail);
        }

        //GET: Movies/Create
        public async Task<IActionResult> Create()
        {
            var productDropdownsData = await _service.GetNewProductDropdownsValues();

            ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
            ViewBag.Cites = new SelectList(productDropdownsData.Cities, "Id", "FullName");
            ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "FullName");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NewProductVM product)
        {
            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
                ViewBag.Cities = new SelectList(productDropdownsData.Cities, "Id", "FullName");
                ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "FullName");

                return View(product);
            }


            await _service.AddNewProductAsync(product);
            return RedirectToAction(nameof(Index));
        }


        //GET: Movies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var productDetails = await _service.GetProductByIdAsync(id);
            if (productDetails == null) return View("NotFound");

            var response = new NewProductVM()
            {
                Id = productDetails.Id,
                Name = productDetails.Name,
                Description = productDetails.Description,
                Price = productDetails.Price,
                StartDate = productDetails.StartDay,
                EndDate = productDetails.EndDay,
                ImageURL = productDetails.ImageURL,
                ProductCategory = productDetails.ProductCategory,
                StoreId = productDetails.StoreId,
                CityId = productDetails.CityId,
                CompanyIds = productDetails.Company_Products.Select(n => n.CompanyId).ToList(),
            };

            var productDropdownsData = await _service.GetNewProductDropdownsValues();
            ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
            ViewBag.Cities = new SelectList(productDropdownsData.Cities, "Id", "FullName");
            ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "FullName");

            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, NewProductVM product)
        {
            if (id != product.Id) return View("NotFound");

            if (!ModelState.IsValid)
            {
                var productDropdownsData = await _service.GetNewProductDropdownsValues();

                ViewBag.Stores = new SelectList(productDropdownsData.Stores, "Id", "Name");
                ViewBag.Cities = new SelectList(productDropdownsData.Cities, "Id", "FullName");
                ViewBag.Companies = new SelectList(productDropdownsData.Companies, "Id", "FullName");

                return View(product);
            }

            await _service.UpdateProductAsync(product);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")] // Đảm bảo chỉ Admin có quyền xóa
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _service.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            await _service.DeleteAsync(id);

            return RedirectToAction("Index");
        }
    }
}

using eCommerce.Data;
using eCommerce.Data.Services;
using eCommerce.Data.Static;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class StoresController : Controller
    {

        private readonly IStoresService _service;

        public StoresController(IStoresService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allStores = await _service.GetAllAsync();
            return View(allStores);
        }
        //Get: Stores/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Logo,Name,Description")] Store store)
        {
            try
            {
                await _service.AddAsync(store);
                await _service.SaveChangesAsync();
                TempData["SuccessMessage"] = "Added a new Store"; // Đặt TempData sau khi thêm thành công
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {            
                return View(store);
            }
        }

        //Get: Stores/Details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var storeDetails = await _service.GetByIdAsync(id);
            if (storeDetails == null) return View("NotFound");
            return View(storeDetails);
        }

        //Get: Stores/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var storeDetails = await _service.GetByIdAsync(id);
            if (storeDetails == null) return View("NotFound");
            return View(storeDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Logo,Name,Description")] Store store)
        {
            try
            { //if (!ModelState.IsValid) return View(store);
                await _service.UpdateAsync(id, store);
                await _service.SaveChangesAsync();
                TempData["SuccessMessage"] = "Edited a Store Successful";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) {
                return View(store);
            }
            
        }

        //Get: Stores/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var storeDetails = await _service.GetByIdAsync(id);
            if (storeDetails == null) return View("NotFound");
            return View(storeDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            var storeDetails = await _service.GetByIdAsync(id);
            if (storeDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            TempData["SuccessMessage"] = "Deleted a Store Successful";
            return RedirectToAction(nameof(Index));
        }
    }
}

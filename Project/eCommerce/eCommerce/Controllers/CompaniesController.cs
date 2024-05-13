using eCommerce.Data;
using eCommerce.Data.Services;
using eCommerce.Data.Static;
using eCommerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class CompaniesController : Controller
    {
        private readonly ICompaniesService _service;

        public CompaniesController(ICompaniesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var data = await _service.GetAllAsync();
            return View(data);
        }

        //Get: Companies/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Create([Bind("FullName, ProfilePicture, Bio")]Company company)
        {
            try
            {
                await _service.AddAsync(company);
                await _service.SaveChangesAsync();
                TempData["SuccessMessage"] = "Added a new Company"; // Đặt TempData sau khi thêm thành công
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(company);
            }
        }

        // Get: Companies/Details/1
        public async Task<IActionResult> Details(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            
            if(companyDetails == null) return View("Not found");

            return View(companyDetails);
        }

        //Get: Companies/Edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("Not found");
            return View(companyDetails);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(int id, [Bind("Id, FullName, ProfilePicture, Bio")] Company company)
        {
            try
            {
                await _service.UpdateAsync(id, company);
                await _service.SaveChangesAsync();
                TempData["SuccessMessage"] = "Successful Edited Your Company"; // Đặt TempData sau khi thêm thành công
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(company);
            }
        }

        //Get: Companies/Delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("Not found");
            return View(companyDetails);
        }

        [HttpPost, ActionName("Delete")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyDetails = await _service.GetByIdAsync(id);
            if (companyDetails == null) return View("Not found");

            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            TempData["SuccessMessage"] = "Successful Deleted Your Company";
            return RedirectToAction(nameof(Index));
        }
    }
}

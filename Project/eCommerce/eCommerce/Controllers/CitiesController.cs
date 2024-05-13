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
    public class CitiesController : Controller
    {
        private readonly ICitiesService _service;

        public CitiesController(ICitiesService service)
        {
            _service = service;
        }

        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            var allCities = await _service.GetAllAsync();
            return View(allCities);
        }
        //GET: producers/details/1
        [AllowAnonymous]
        public async Task<IActionResult> Details(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");
            return View(cityDetails);
        }

        //GET: producers/create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("ProfilePicture,FullName,Bio")] City city)
        {
            try
            {
                await _service.AddAsync(city);
                await _service.SaveChangesAsync();
                TempData["SuccessMessage"] = "Added a new City";//Đặt TempData
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(city);
            }

        }

        //GET: producers/edit/1
        public async Task<IActionResult> Edit(int id)
        {
            var producerDetails = await _service.GetByIdAsync(id);
            if (producerDetails == null) return View("NotFound");
            return View(producerDetails);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfilePicture,FullName,Bio")] City city)
        {
            try
            {
                await _service.UpdateAsync(id, city);
                await _service.SaveChangesAsync();
                TempData["SuccessMessage"] = "Successful edited your city";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(city);
            }
        }

        //GET: cities/delete/1
        public async Task<IActionResult> Delete(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");
            return View(cityDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cityDetails = await _service.GetByIdAsync(id);
            if (cityDetails == null) return View("NotFound");

            await _service.DeleteAsync(id);            
            await _service.SaveChangesAsync();
            TempData["SuccessMessage"] = "Deleted a City Successful";
            return RedirectToAction(nameof(Index));
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracaInz.Services;
using PracaInz.ViewModels.DevicesViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using PracaInz.BLL;

namespace PracaInz.Web.Controllers
{
    public class DevicesController : Controller
    {
        private DeviceServices _deviceServices;
        private CategoryServices _categoryServices;
        private UserManager<User> _userManager;

        public DevicesController(DeviceServices deviceServices,
            CategoryServices categoryServices,
            UserManager<User> userManager)
        {
            _deviceServices = deviceServices;
            _categoryServices = categoryServices;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = _deviceServices.GetAllDevices();
            return View(vm);
        }

        public IActionResult Delete(int id)
        {
            var vm = _deviceServices.GetDevice(id);
            return View(vm);
        }
        public IActionResult DeleteConfirmed(int id)
        {
            _deviceServices.DeleteDevice(id);
            return RedirectToAction("Index", "Devices");
        }

        [HttpGet]
        public IActionResult Add()
        {
            // Kawałek kodu który odpowiada za probranie z bazy wszystkich kategorii
            // i wrzucenie ich do rozwijanej listy która jest dostępna w View 
            // odpowiadającym za dodawanie
            var categories = _categoryServices.ReturnAllCategoryToDropDown();

            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            });
            return View();
        }

        public async Task<IActionResult> AddAsync(NewDeviceViewModel data)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);
            
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _deviceServices.AddAsync(data.Manufacturer,
                data.Model,
                data.SerialNumber,
                data.DeviceDescription,
                user.Id,
                data.CategoryId);
            return RedirectToAction("Index", "Devices");
        }
    }
}

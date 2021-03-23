using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PracaInz.BLL;
using PracaInz.Services;
using PracaInz.ViewModels.NetworkDevicesViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Web.Controllers
{
    public class NetworkDevicesController : Controller
    {
        private NetworkDeviceServices _networkDeviceServices;
        private CategoryServices _categoryServices;
        private UserManager<User> _userManager;
        private UserRoleIdentityServices _userRoleIdentityServices;

        public NetworkDevicesController(NetworkDeviceServices networkDeviceServices,
            CategoryServices categoryServices,
            UserManager<User> userManager,
            UserRoleIdentityServices userRoleIdentityServices)
        {
            _userRoleIdentityServices = userRoleIdentityServices;
            _networkDeviceServices = networkDeviceServices;
            _categoryServices = categoryServices;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var vm = _networkDeviceServices.GetAllDevices();
            return View(vm);
        }

        public IActionResult Delete(int id)
        {
            var vm = _networkDeviceServices.GetDevice(id);
            return View(vm);
        }

        public IActionResult DeleteConfirmed(int id)
        {
            _networkDeviceServices.DeleteDevice(id);
            return RedirectToAction("Index", "NetworkDevices");
        }
        
        [HttpGet]
        public IActionResult Add()
        {
            var categories = _categoryServices.ReturnAllCategoryToDropDown();
            var users = _userRoleIdentityServices.ReturnAllUsersToDropDown();

            ViewBag.Users = users.Select(y => new SelectListItem()
            {
                Text = y.FirstName,
                Value = y.Id.ToString()
            });
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            });
            return View();
        }

        public async Task<IActionResult> AddAsync(NewNetworkDeviceViewModel data)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var user = await _userManager.GetUserAsync(User);

            if (!ModelState.IsValid)
            {
                return View(data);
            }
            await _networkDeviceServices.AddAsync(data.Manufacturer,
                data.Model,
                data.SerialNumber,
                data.DeviceDescription,
                data.UserId,
                data.CategoryId,
                data.IPAddress);
            return RedirectToAction("Index", "Devices");
        }

        public IActionResult DevicePingStatus(int id)
        {
            _networkDeviceServices.DevicePingStatus(id);
            return RedirectToAction("Index", "Devices");
        }
    }
}

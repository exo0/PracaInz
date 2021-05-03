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
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace PracaInz.Web.Controllers
{
    [Authorize(Roles = "Administrator,HelpDesk,User")]
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
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            var vm = _networkDeviceServices.GetAllDevices();
            return View(vm);
        }
        [HttpGet]
        [Authorize(Roles ="Administrator")]
        public IActionResult Delete(int id)
        {
            var vm = _networkDeviceServices.GetDevice(id);
            return View(vm);
        }
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteConfirmed(int id)
        {
            _networkDeviceServices.DeleteDevice(id);
            return RedirectToAction("Index", "NetworkDevices");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var categories = _categoryServices.ReturnAllCategoryToDropDown();
            ViewBag.Categories = categories.Select(x => new SelectListItem()
            {
                Text = x.Title,
                Value = x.Id.ToString()
            });
            var users = _userRoleIdentityServices.ReturnAllUsersToDropDown();
            ViewBag.Userss = users.Select(x => new SelectListItem()
            {
                Text = x.FirstName + x.LastName,
                Value = x.Id.ToString()
            });
            var vm = _networkDeviceServices.GetDevice(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditDevice(int id, string manufacturer, string model, string serialNumber, string deviceDescription, int UserId, int CategoryId,string IPAddress)
        {
            await _networkDeviceServices.EditAsync(id, manufacturer, model, serialNumber, deviceDescription, UserId, CategoryId,IPAddress);
            return RedirectToAction("Index", "Devices");
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

        [HttpGet]
        [Authorize(Roles="Administrator,HelpDesk")]
        public IActionResult DevicePingStatus(int id)
        {
            _networkDeviceServices.DevicePingStatus(id);
            return RedirectToAction("Index","Devices");
        }
    }
}

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
using Microsoft.AspNetCore.Authorization;

namespace PracaInz.Web.Controllers
{
    [Authorize(Roles = "Administrator,HelpDesk,User")]
    public class DevicesController : Controller
    {
        private DeviceServices _deviceServices;
        private CategoryServices _categoryServices;
        private UserManager<User> _userManager;
        private UserRoleIdentityServices _userRoleIdentityServices;

        public DevicesController(DeviceServices deviceServices,
            CategoryServices categoryServices,
            UserManager<User> userManager,
            UserRoleIdentityServices userRoleIdentityServices)
        {
            _userRoleIdentityServices = userRoleIdentityServices;
            _deviceServices = deviceServices;
            _categoryServices = categoryServices;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {
            var vm = _deviceServices.GetAllDevices();
            return View(vm);
        }
        [HttpGet]
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
        public IActionResult GetYourDevices(int id)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var devices = _deviceServices.GetAllDevicesFilterByUserId(User.Identity.Name);
            return PartialView("~/Views/Devices/Index.cshtml", devices);
        }
        [Authorize(Roles = "Administrator")]
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
        [Authorize(Roles = "Administrator")]
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
                data.UserId,
                data.CategoryId);
            return RedirectToAction("Index", "Devices");
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
            ViewBag.Userss = users.Select(x=> new SelectListItem()
            {
                Text = x.FirstName + x.LastName,
                Value = x.Id.ToString()
            });
            var vm = _deviceServices.GetDevice(id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditDevice(int id, string manufacturer,string model, string serialNumber, string deviceDescription,int UserId, int CategoryId)
        {
            _deviceServices.UpdateDevice(id, manufacturer, model, serialNumber, deviceDescription, CategoryId, UserId);
            return RedirectToAction("Index", "Devices");
        }

    }
}

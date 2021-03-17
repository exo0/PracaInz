using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PracaInz.BLL;
using PracaInz.Services;
using PracaInz.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Web.Controllers
{
    public class HomeController : Controller
    {
        private TicketServices _ticketServices;
        private DeviceServices _deviceServices;
        private UserManager<User> _userManager;
        
        private readonly ILogger<HomeController> _logger;
        

        public HomeController(ILogger<HomeController> logger, TicketServices ticketServices, UserManager<User> userManager, DeviceServices DeviceServices)
        {
            _logger = logger;
            _ticketServices = ticketServices;
            _userManager = userManager;
            _deviceServices = DeviceServices;

        }

        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

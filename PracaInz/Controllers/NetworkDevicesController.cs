﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracaInz.Web.Controllers
{
    public class NetworkDevicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
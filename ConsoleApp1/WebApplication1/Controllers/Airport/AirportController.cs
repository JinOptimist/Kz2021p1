using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers.Airport
{
    public class AirportController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CitizenController : Controller
    {
        public static List<FullProfileViewModel> Users = new List<FullProfileViewModel>();

        public IActionResult Index()
        {
            return View(Users);
        }

        [HttpGet]
        public IActionResult FullProfile()
        {
            var model = new FullProfileViewModel() { 
                Age = 30,
                Job = "Строитель",
                Name = "Иванов",
                RegistrationDate = DateTime.Now.AddDays(-20)
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult CreateUser(FullProfileViewModel newUser)
        {
            newUser.RegistrationDate = DateTime.Now;
            Users.Add(newUser);
            return RedirectToAction("Index");
        }

        public JsonResult Remove(string name)
        {
            Thread.Sleep(2000);

            var userForDelete = Users.SingleOrDefault(x => x.Name == name);
            if (userForDelete == null)
            {
                return Json(false);
            }

            Users.Remove(userForDelete);
            return Json(true);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.EfStuff.Model;
using WebApplication1.EfStuff.Repositoryies;
using WebApplication1.Models;
using System.IO;

namespace WebApplication1.Controllers
{
    public class SportEventController : Controller
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        private SportEventRepository _SportEventRepository;
        public SportEventController(SportEventRepository sportEventRepository, IWebHostEnvironment hostEnvironment)
        {
            _SportEventRepository = sportEventRepository;
            this._hostEnvironment = hostEnvironment;
        }
        public ActionResult Index()
        {
            var viewModels = _SportEventRepository.GetAll()
                .Select(x => new SportEventViewModel()
                {
                    Id = x.Id,
                    title = x.title,
                    description = x.description,
                    img = x.img,
                    date = x.date
                }).ToList();
            return View(viewModels);
        }

        // GET: SportEventController/Details/5
        [HttpGet]
        public IActionResult AddEvent()
        {
            return View();
        }

        // GET: SportEventController/Create
        [HttpPost]
        public async Task<ActionResult> CreateEvent(SportEventViewModel newEvent)
        {
            if (ModelState.IsValid)
            {
                string wwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(newEvent.imagefile.FileName);
                string extension = Path.GetExtension(newEvent.imagefile.FileName);
                fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                newEvent.img = fileName;
                string path = Path.Combine(wwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await newEvent.imagefile.CopyToAsync(fileStream);
                }
            }
            var _event = new SportEvent()
            {
                title = newEvent.title,
                description = newEvent.description,
                date = newEvent.date,
                img = newEvent.img
            };

            _SportEventRepository.Save(_event);

            return RedirectToAction("Index");
        }
        public ActionResult ShowEvent(long? id)
        {
            var _event = _SportEventRepository.Get((long)id);
            var newEvent = new SportEventViewModel();
            newEvent.title = _event.title;
            newEvent.description = _event.description;
            return View(newEvent);
        }
 
        // POST: SportEventController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SportEventController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SportEventController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: SportEventController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SportEventController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

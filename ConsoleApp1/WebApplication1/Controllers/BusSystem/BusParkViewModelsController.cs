using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication1.EfStuff;
using WebApplication1.Models;

namespace WebApplication1.Controllers.BusSystem
{
    public class BusParkViewModelsController : Controller
    {
        //private readonly KzDbContext _context;

        //public BusParkViewModelsController(KzDbContext context)
        //{
        //    _context = context;
        //}

        //// GET: BusParkViewModels
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.BusParkViewModel.ToListAsync());
        //}

        //// GET: BusParkViewModels/Details/5
        //public async Task<IActionResult> Details(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var busParkViewModel = await _context.BusParkViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (busParkViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(busParkViewModel);
        //}

        //// GET: BusParkViewModels/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: BusParkViewModels/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Model,Type,Capacity,Price")] BusParkViewModel busParkViewModel)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(busParkViewModel);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(busParkViewModel);
        //}

        //// GET: BusParkViewModels/Edit/5
        //public async Task<IActionResult> Edit(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var busParkViewModel = await _context.BusParkViewModel.FindAsync(id);
        //    if (busParkViewModel == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(busParkViewModel);
        //}

        //// POST: BusParkViewModels/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for 
        //// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("Id,Model,Type,Capacity,Price")] BusParkViewModel busParkViewModel)
        //{
        //    if (id != busParkViewModel.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(busParkViewModel);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!BusParkViewModelExists(busParkViewModel.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(busParkViewModel);
        //}

        //// GET: BusParkViewModels/Delete/5
        //public async Task<IActionResult> Delete(long? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var busParkViewModel = await _context.BusParkViewModel
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (busParkViewModel == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(busParkViewModel);
        //}

        //// POST: BusParkViewModels/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(long id)
        //{
        //    var busParkViewModel = await _context.BusParkViewModel.FindAsync(id);
        //    _context.BusParkViewModel.Remove(busParkViewModel);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool BusParkViewModelExists(long id)
        //{
        //    return _context.BusParkViewModel.Any(e => e.Id == id);
        //}
    }
}

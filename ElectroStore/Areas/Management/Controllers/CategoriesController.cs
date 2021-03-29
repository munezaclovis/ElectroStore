using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectroStore.Data;
using ElectroStore.Models;

namespace ElectroStore.Areas.Management.Controllers
{
    [Area("Management")]
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.ToListAsync());
        }

        //GET: Category/Create
        public IActionResult Add()
        {
            Category category = new Category();
            return PartialView("Add", category);
        }

        // POST: Categorys/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,Deleted")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                var result = await _context.SaveChangesAsync();
                return Json(new { Status = "Success", category });
            }
            return PartialView("Add", category);
        }

        // GET: Categorys/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Category with that id was not found" });
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return Json(new { Status = "Error", Message = "Category with that id was not found" });
            }
            return PartialView("Edit", category);
        }

        // POST: Categorys/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Deleted")] Category category)
        {
            if (id != category.Id)
            {
                return Json(new { Status = "Error", Message = "Id Can't be null" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.Id))
                    {
                        return Json(new { Status = "Error", Message = "Category with that id was not found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { Status = "Success", category });
            }
            return PartialView("Edit", category);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Id can't be empty" });
            }

            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category == null)
            {
                return Json(new { Status = "Error", Message = "Category with that Id does not exist in the database" });
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();

            return Json(new { Status = "Success", Id = category.Id, Message = "Category Successfully Deleted" });
        }

        [HttpPost]
        public IActionResult Restore(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Id can't be empty" });
            }

            var category = _context.Categories.Where(x => x.Id == id).FirstOrDefault();
            if (category == null)
            {
                return Json(new { Status = "Error", Message = "Category with that Id does not exist in the database" });
            }
            category.Deleted = false;
            _context.Categories.Update(category);
            _context.SaveChanges();
            return Json(new { Status = "Success", Id = category.Id, Message = "Category Successfully Restored" });
        }

        private bool CategoryExists(string id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}

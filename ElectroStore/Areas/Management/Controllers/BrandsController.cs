using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ElectroStore.Data;
using ElectroStore.Models;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ElectroStore.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Manager, Employee")]
    public class BrandsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BrandsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Brands.ToListAsync());
        }

        //GET: Brands/Create
        public IActionResult Add()
        {
            Brand brand = new Brand();
            return PartialView("Add", brand);
        }

        // POST: Brands/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,Deleted")] Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brand);
                await _context.SaveChangesAsync();
                return Json(new { Status = "Success", id = brand.Id, brand });
            }
            return PartialView("Add", brand);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Brand with that id was not found" });
            }

            var brand = await _context.Brands.FindAsync(id);
            if (brand == null)
            {
                return Json(new { Status = "Error", id = brand.Id, Message = "Brand with that id was not found" });
            }
            return PartialView("Edit", brand);
        }

        // POST: Brands/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,Deleted")] Brand brand)
        {
            if (id != brand.Id)
            {
                return Json(new { Status = "Error", id = brand.Id, Message = "Id Can't be null" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brand);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BrandExists(brand.Id))
                    {
                        return Json(new { Status = "Error", id = brand.Id, Message = "Brand with that id was not found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { Status = "Success", id = brand.Id, brand });
            }
            return PartialView("Edit", brand);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Id can't be empty" });
            }

            var brand = _context.Brands.Where(x => x.Id == id).FirstOrDefault();
            if (brand == null)
            {
                return Json(new { Status = "Error", id = brand.Id, Message = "Brand with that Id does not exist in the database" });
            }
            _context.Brands.Remove(brand);
            _context.SaveChanges();

            return Json(new { Status = "Success", Id = brand.Id, Message = "Brand Successfully Deleted" });
        }

        [HttpPost]
        public IActionResult Restore(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Id can't be empty" });
            }

            var brand = _context.Brands.Where(x => x.Id == id).FirstOrDefault();
            if (brand == null)
            {
                return Json(new { Status = "Error", id = brand.Id, Message = "Brand with that Id does not exist in the database" });
            }
            brand.Deleted = false;
            _context.Brands.Update(brand);
            _context.SaveChanges();
            return Json(new { Status = "Success", Id = brand.Id, Message = "Brand Successfully Restored" });
        }

        private bool BrandExists(string id)
        {
            return _context.Brands.Any(e => e.Id == id);
        }
    }
}

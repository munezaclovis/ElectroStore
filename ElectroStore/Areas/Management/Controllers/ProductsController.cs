using ElectroStore.Data;
using ElectroStore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Areas.Management.Controllers
{
    [Area("Management")]
    [Authorize(Roles = "Manager, Employee")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Brands
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.Include(x => x.Brand).Include(x => x.Category).ToListAsync());
        }

        //GET: Brands/Create
        public IActionResult Add()
        {
            Product product = new Product();
            ViewBag.Brands = _context.Brands.Select(x => new SelectListItem { Text = x.Name, Value = x.Id });
            ViewBag.Categories = _context.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id });
            return PartialView("Add", product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add([Bind("Name,BrandId,CategoryId,DateAdded,DateUpdated,ImageURLs,Price,Deleted")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return Json(new { Status = "Success", id = product.Id, product });
            }
            return PartialView("Add", product);
        }

        // GET: Brands/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Product with that id was not found" });
            }

            var product = await _context.Products.FindAsync(id);
            ViewBag.Brands = _context.Brands.Select(x => new SelectListItem { Text = x.Name, Value = x.Id });
            ViewBag.Categories = _context.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id });
            if (product == null)
            {
                return Json(new { Status = "Error", id = product.Id, Message = "Product with that id was not found" });
            }
            return PartialView("Edit", product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,Name,BrandId,CategoryId,DateAdded,DateUpdated,ImageURLs,Price,Deleted")] Product product)
        {
            if (id != product.Id)
            {
                return Json(new { Status = "Error", id = product.Id, Message = "Id Can't be null" });
            }

            ViewBag.Brands = _context.Brands.Select(x => new SelectListItem { Text = x.Name, Value = x.Id });
            ViewBag.Categories = _context.Categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id });

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return Json(new { Status = "Error", id = product.Id, Message = "product with that id was not found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { Status = "Success", id = product.Id, product });
            }
            return PartialView("Edit", product);
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Id can't be empty" });
            }

            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return Json(new { Status = "Error", id = product.Id, Message = "Product with that Id does not exist in the database" });
            }
            _context.Products.Remove(product);
            _context.SaveChanges();

            return Json(new { Status = "Success", Id = product.Id, Message = "Product Successfully Deleted" });
        }

        [HttpPost]
        public IActionResult Restore(string id)
        {
            if (id == null)
            {
                return Json(new { Status = "Error", Message = "Id can't be empty" });
            }

            var product = _context.Products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return Json(new { Status = "Error", id = product.Id, Message = "Product with that Id does not exist in the database" });
            }
            product.Deleted = false;
            _context.Products.Update(product);
            _context.SaveChanges();
            return Json(new { Status = "Success", Id = product.Id, Message = "Product Successfully Restored" });
        }

        private bool ProductExists(string id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}

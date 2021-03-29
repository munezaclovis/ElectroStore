using ElectroStore.Data;
using ElectroStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Controllers
{
    public class ProductsController : Controller
    {
        public readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync(string id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            Product product = await _context.Products.Include(x => x.Brand).Include(x => x.Category).Where(x => x.Id.Contains(id)).FirstAsync();
            if (product == null)
            {
                return RedirectToAction("Index", "Home", new { area = "" });
            }
            return View(product);
        }
    }
}

using ElectroStore.Data;
using ElectroStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Controllers
{
    public class CartController : Controller
    {
        public readonly ApplicationDbContext _context;
        public readonly UserManager<User> _userManager;

        public CartController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (!CartExists(_userManager.GetUserId(User)))
            {
                var newCart = new Cart { Id = Guid.NewGuid().ToString(), UserID = _userManager.GetUserId(User) };
                _context.Add(newCart);
                _context.SaveChanges();
            }
            Cart UserCartItems = await _context.Carts.Include(x => x.CartItems).ThenInclude(x => x.Product).Where(x => x.UserID.Contains(_userManager.GetUserId(User))).FirstOrDefaultAsync();
            return View(UserCartItems);
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("ProductId,Quantity,UserId")] CartItem cartItem)
        {
            if (ModelState.IsValid)
            {
                if (!CartExists(_userManager.GetUserId(User)))
                {
                    var newCart = new Cart { Id = Guid.NewGuid().ToString(), UserID = _userManager.GetUserId(User) };
                    _context.Add(newCart);
                    _context.SaveChanges();
                }
                cartItem.CartId = _context.Carts.Where(x => x.UserID.Contains(_userManager.GetUserId(User))).FirstOrDefault().Id;

                _context.Add(cartItem);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index", "Home", new { area = "" });
        }


        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid)
            {
                var cartItem = _context.CartItems.Find(id);
                if (cartItem != null)
                {
                    _context.Remove(cartItem);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction("Index", "Cart", new { area = "" });
        }

        private bool CartExists(string UserID)
        {
            return _context.Carts.Any(e => e.UserID == UserID);
        }
    }
}

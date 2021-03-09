using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SpareParts.Data;
using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Controllers
{
    [Route("Orders")]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        public OrdersController(ApplicationDbContext db, UserManager<User> userManager)
        {

            _db = db;
            _userManager = userManager;
        }

        // GET: Reviews
        [Authorize]
        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            bool isAdmin = await _userManager.IsInRoleAsync(user, "Admin");

            if (isAdmin)
            {
                var allOrders = await _db.Orders.Include(o => o.OrderLines).Include(o => o.User).ToListAsync();
                return View(allOrders);
            }
            else
            {
                var orders = await _db.Orders.Include(o => o.OrderLines).Include(o => o.User)
                    .Where(r => r.User == user).ToListAsync();
                return View(orders);
            }
        }

        // GET: Orders/Details/5
        [Authorize]
        [Route("Details")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            var orders = await _db.Orders.Include(o => o.OrderLines).Include(o => o.User)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var userRoles = await _userManager.GetRolesAsync(user);
            bool isAdmin = userRoles.Any(r => r == "Admin");

            if (orders == null)
            {
                return NotFound();
            }

            if (isAdmin == false)
            {
                var userId = _userManager.GetUserId(HttpContext.User);
                if (orders.UserId != userId)
                {
                    return BadRequest("You do not have permissions to view this order.");
                }
            }

            var orderDetailsList = _db.OrderDetails.Include(o => o.Product).Include(o => o.Order)
                .Where(x => x.OrderId == orders.OrderId);

            ViewBag.OrderDetailsList = orderDetailsList;

            return View(orders);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Admin")]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _db.Orders.Include(o => o.User)
                .SingleOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }



        // POST: OrdersTest/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Route("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _db.Orders.SingleOrDefaultAsync(m => m.OrderId == id);
            _db.Orders.Remove(order);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}

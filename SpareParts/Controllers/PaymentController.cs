using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SpareParts.Data;
using SpareParts.Models;
using SpareParts.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Controllers
{
    [Route("payment")]
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private readonly IOrderRepository _orderRepository;
        private readonly ShoppingCart _shoppingCart;

        private readonly UserManager<User> _userManager;
        public PaymentController(IOrderRepository orderRepository,
             ShoppingCart shoppingCart,ApplicationDbContext _db, UserManager<User> userManager)
        {
            db = _db;
            _orderRepository = orderRepository;
            _shoppingCart = shoppingCart;
            _userManager = userManager;
        }
        [Authorize]
        public IActionResult Checkout()
        {
            return View();
        }

        [Authorize]
        //[HttpPost]
        [Route("checkout")]
        public async Task<IActionResult> Checkout(Order order)
        {
            var userId = _userManager.GetUserId(HttpContext.User);
            order.UserId = userId;

            var items = await _shoppingCart.GetShoppingCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            if (_shoppingCart.ShoppingCartItems.Count == 0)
            {
                ModelState.AddModelError("", "Your cart is empty, add some products first");
            }

            if (ModelState.IsValid)
            {
                await _orderRepository.CreateOrderAsync(order);
                await _shoppingCart.ClearCartAsync();

                return RedirectToAction("CheckoutComplete");
            }

            return View(order);
        }

        [Authorize]
        [Route("checkoutcomplete")]
        public IActionResult CheckoutComplete()
        {
            ViewBag.CheckoutCompleteMessage = $"Thanks for your order, We'll deliver your products soon!";
            return View();
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("index")]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

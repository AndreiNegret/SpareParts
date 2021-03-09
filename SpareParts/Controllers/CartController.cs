using Microsoft.AspNetCore.Mvc;
using SpareParts.Data;
using SpareParts.Models;
using SpareParts.Repository.Interfaces;
using SpareParts.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Controllers
{
    [Route("cart")]
    public class CartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private readonly ShoppingCart _shoppingCart;
        private readonly IProductRepository _productRepository;
        public CartController(ApplicationDbContext _db, ShoppingCart shoppingCart, IProductRepository productRepository)
        {
            db = _db;
            _shoppingCart = shoppingCart;
            _productRepository = productRepository;
        }

        [Route("")]
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            var items = await _shoppingCart.GetShoppingCartItemsAsync();
            _shoppingCart.ShoppingCartItems = items;

            var shoppingCartViewModel = new ShoppingCartViewModel
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()

            };

            return View(shoppingCartViewModel);
        }
        [Route("buy/{productId}")]
        [Route("AddToShoppingCart/{productId}")]
        public async Task<IActionResult> AddToShoppingCart(int productId)
        {
            var selectedProduct = await _productRepository.GetByIdAsync(productId);

            if (selectedProduct != null)
            {
                await _shoppingCart.AddToCartAsync(selectedProduct, 1);
            }
            return RedirectToAction("Index", "Cart");
        }

        [Route("RemoveFromShoppingCart/{productId}")]
        public async Task<IActionResult> RemoveFromShoppingCart(int productId)
        {
            var selectedProduct = await _productRepository.GetByIdAsync(productId);

            if (selectedProduct != null)
            {
                await _shoppingCart.RemoveFromCartAsync(selectedProduct);
            }
            return RedirectToAction("Index", "Cart");
        }

        [Route("clearcart")]
        public async Task<IActionResult> ClearCart()
        {
            await _shoppingCart.ClearCartAsync();

            return RedirectToAction("Index", "Cart");
        }
    }
}

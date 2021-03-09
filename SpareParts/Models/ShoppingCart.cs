using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SpareParts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Models
{
    public class ShoppingCart
    {
        private readonly ApplicationDbContext _db;

        private ShoppingCart(ApplicationDbContext db)
        {
            _db = db;
        }

        public string ShoppingCartId { get; set; }

        public List<ShoppingCartItem> ShoppingCartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?
                .HttpContext.Session;

            var context = services.GetService<ApplicationDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public async Task AddToCartAsync(Product product, int amount)
        {
            var shoppingCartItem =
                    await _db.ShoppingCartItems.SingleOrDefaultAsync(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem
                {
                    ShoppingCartId = ShoppingCartId,
                    Product = product,
                    Amount = 1
                };

                _db.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            await _db.SaveChangesAsync();
        }

        public async Task<int> RemoveFromCartAsync(Product product)
        {
            var shoppingCartItem =
                    await _db.ShoppingCartItems.SingleOrDefaultAsync(
                        s => s.Product.ProductId == product.ProductId && s.ShoppingCartId == ShoppingCartId);

            var localAmount = 0;

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else
                {
                    _db.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            await _db.SaveChangesAsync();

            return localAmount;
        }

        public async Task<List<ShoppingCartItem>> GetShoppingCartItemsAsync()
        {
            return ShoppingCartItems ??
                   (ShoppingCartItems = await
                       _db.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                           .Include(s => s.Product)
                           .ToListAsync());
        }

        public async Task ClearCartAsync()
        {
            var cartItems = _db
                .ShoppingCartItems
                .Where(cart => cart.ShoppingCartId == ShoppingCartId);

            _db.ShoppingCartItems.RemoveRange(cartItems);

            await _db.SaveChangesAsync();
        }

        public decimal GetShoppingCartTotal()
        {
            var total = _db.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                .Select(c => c.Product.Price * c.Amount).Sum();
            return (decimal)total;
        }

    }
}

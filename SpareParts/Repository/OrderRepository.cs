using SpareParts.Data;
using SpareParts.Models;
using SpareParts.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly ShoppingCart _shoppingCart;

        public OrderRepository(ApplicationDbContext db, ShoppingCart shoppingCart)
        {
            _db = db;
            _shoppingCart = shoppingCart;
        }

        public async Task CreateOrderAsync(Order order)
        {
            order.OrderPlaced = DateTime.Now;
            decimal totalPrice = 0M;

            var shoppingCartItems = _shoppingCart.ShoppingCartItems;

            foreach (var shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail()
                {
                    Amount = shoppingCartItem.Amount,
                    ProductId = shoppingCartItem.Product.ProductId,
                    Order = order,
                    Price = (decimal)shoppingCartItem.Product.Price,
                };
                totalPrice += orderDetail.Price * orderDetail.Amount;
                _db.OrderDetails.Add(orderDetail);
            }

            order.OrderTotal = totalPrice;
            _db.Orders.Add(order);

            await _db.SaveChangesAsync();
        }


    }
}
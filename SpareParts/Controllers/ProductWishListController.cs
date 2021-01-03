using Microsoft.AspNetCore.Mvc;
using SpareParts.Areas.Administrator.ClassModels;
using SpareParts.Data;
using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.Controllers
{
    public class ProductWishListController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ProductWishListController(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<IActionResult> AddProduct(ProductWishListRequest productWishlistRquest)
        {

            string userId = "";
            ProductWishList productWishlist = new ProductWishList()
            {
                ProductId = productWishlistRquest.ProductId,
                UserId = userId
            };

            await db.ProductWishLists.AddAsync(productWishlist);
            db.SaveChanges();

            return Ok();
        }

        public IActionResult DeleteProduct(ProductWishListRequest productWishlistRquest)
        {
            string userId = "";
            ProductWishList productWishlist = db.ProductWishLists.Where(c => c.UserId == userId && c.ProductId == productWishlistRquest.ProductId).FirstOrDefault();

            db.ProductWishLists.Remove(productWishlist);
            db.SaveChanges();

            return Ok();
        }

        public IActionResult GetProductWishList(string userId)
        {
            List<ProductWishList> productWishlist = new List<ProductWishList>();
            productWishlist = db.ProductWishLists.Where(c => c.UserId == userId).ToList();
            
            return Ok(productWishlist);
        }
    }
}

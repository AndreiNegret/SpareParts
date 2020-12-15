using Microsoft.AspNetCore.Mvc;
using SpareParts.Data;
using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.ViewComponents
{
    [ViewComponent(Name = "Product")]
    public class ProductViewComponents : ViewComponent
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ProductViewComponents(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> products = db.Products.ToList();

            return View("Index", products);
        }
    }

}

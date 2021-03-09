using Microsoft.AspNetCore.Mvc;
using SpareParts.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.ViewComponents
{
    [ViewComponent(Name = "CartTop")]
    public class CartTopViewComponent : ViewComponent
    {
        private readonly ApplicationDbContext _db = new ApplicationDbContext();

        public CartTopViewComponent(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            //List<Item> cart = _db.Items.ToList();
            //ViewBag.cart = cart;
            //ViewBag.countItems = cart.Count;
            //ViewBag.Total = cart.Sum(it => it.Price * it.Quantity);
            return View("Index");
        }


    }
}

using Microsoft.AspNetCore.Mvc;
using SpareParts.Data;
using SpareParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpareParts.ViewComponents
{
    [ViewComponent(Name = "Category")]
    public class CategoryViewComponent : ViewComponent
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public CategoryViewComponent(ApplicationDbContext _db)
        {
            db = _db;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Category> categories = db.Categories.ToList();


            return View("Index", categories);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpareParts.Data;
using SpareParts.Models;

namespace SpareParts.Areas.Administrator.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("administrator")]
    [Route("administrator/category")]
    public class CategoryController : Controller
    {

        private ApplicationDbContext db = new ApplicationDbContext();

        public CategoryController(ApplicationDbContext _db)
        {

            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.categories = db.Categories.Where(c => c.Children != null).ToList();
            ViewBag.categories = db.Categories.Where(c => c.ParentCategory == null).ToList();
            return View();
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            var category = new Category();

            return View("Add", category);
        }


        [HttpPost]
        [Route("add")]
        public IActionResult Add(Category category)
        {
            category.ParentCategory = null;
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "administrator" });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "administrator" });
        }


        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);

            return View("Edit", category);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id, Category category)
        {
            var currentCategory = db.Categories.Find(id);
            currentCategory.Name = category.Name;
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "administrator" });
        }


        [HttpGet]
        [Route("addsubcategory/{id}")]
        public IActionResult AddSubcategory(int id)
        {
            var category = new Category()
            {

                ParentCategoryId = id
            };

            return View("AddSubcategory", category);
        }

        [HttpPost]
        [Route("addsubcategory/{id}")]
        public IActionResult AddSubcategory(int id, Category category)
        {
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "administrator" });
        }


    }
}



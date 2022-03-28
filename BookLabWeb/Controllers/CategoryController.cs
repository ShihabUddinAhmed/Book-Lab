using BookLabWeb.Data;
using BookLabWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookLabWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryDbContext _categoryDb;

        public CategoryController(CategoryDbContext categoryDb)
        {
            _categoryDb = categoryDb;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _categoryDb.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "The Display Order cannont be same as the name.");
            }
            if(ModelState.IsValid)
            {
                _categoryDb.Categories.Add(category);
                _categoryDb.SaveChanges();
                TempData["success"] = "Category Created!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _categoryDb.Categories.Find(id);
            if(categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("DisplayOrder", "The Display Order cannont be same as the name.");
            }
            if (ModelState.IsValid)
            {
                _categoryDb.Categories.Update(category);
                _categoryDb.SaveChanges();
                TempData["success"] = "Category Edited!";
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _categoryDb.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var categoryFromDb = _categoryDb.Categories.Find(id);
            if (categoryFromDb == null)
            {
                return NotFound();
            }
            _categoryDb.Categories.Remove(categoryFromDb);
            _categoryDb.SaveChanges();
            TempData["success"] = "Category Deleted!";
            return RedirectToAction("Index");
        }
    }
}

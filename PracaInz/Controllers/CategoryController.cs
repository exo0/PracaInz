using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracaInz.Services;
using PracaInz.ViewModels.CategoryViewModels;

namespace PracaInz.Web.Controllers
{
    public class CategoryController : Controller
    {
        private CategoryServices _categoryService;

        public CategoryController(CategoryServices categoryServices)
        {
            _categoryService = categoryServices;
        }
        
        public IActionResult Index()
        {
            var vm = _categoryService.GetAllCategories();
            return View(vm);
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(NewCategoryItemViewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            _categoryService.Add(data.Title);
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Edit(int id)
        {
            var vm = _categoryService.GetCategory(id);
            return View(vm);
        }

        public IActionResult EditCategory(int id,string title)
        {
            _categoryService.UpdateCategory(id, title);
            return RedirectToAction("Index", "Category");
        }

        public IActionResult Delete(int id)
        {
            var vm = _categoryService.GetCategory(id);
            return View(vm);
        }

        public IActionResult DeleteConfirmed(int id)
        {
            _categoryService.DeleteCat(id);
            return RedirectToAction("Index", "Category");
        }


    }
}

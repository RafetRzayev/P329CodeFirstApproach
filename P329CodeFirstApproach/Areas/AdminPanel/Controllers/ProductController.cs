using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.Areas.AdminPanel.Models;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.DataAccessLayer.Entities;

namespace P329CodeFirstApproach.Areas.AdminPanel.Controllers
{
    public class ProductController : AdminController
    {
        private AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async  Task<IActionResult> Index()
        {
            var products = await _dbContext.Products.ToListAsync();

            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _dbContext.Categories.ToListAsync();

            var selectedCategoryItems = new List<SelectListItem>();

            categories.ForEach(x => selectedCategoryItems.Add(new SelectListItem(x.Name, x.Id.ToString())));

            var model = new CreateProductViewModel
            {
                CategoryListItems = selectedCategoryItems
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            var categories = await _dbContext.Categories.ToListAsync();

            var selectedCategoryItems = new List<SelectListItem>();

            categories.ForEach(x => selectedCategoryItems.Add(new SelectListItem(x.Name, x.Id.ToString())));

            if (!ModelState.IsValid)
            {
                return View(new CreateProductViewModel
                {
                    CategoryListItems = selectedCategoryItems
                });
            }

            var product = new Product
            {
                Name = model.Name,
                Price = model.Price,
                CategoryId = model.CategoryId,
                ImageUrl = ""
            };

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

      
    }
}

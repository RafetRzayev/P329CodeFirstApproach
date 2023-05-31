using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.DataAccessLayer.Entities;

namespace P329CodeFirstApproach.Areas.AdminPanel.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            
            return View(categories);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null) return NotFound();

            return View(category);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Create")]
        public async Task<IActionResult> CreateName(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExist = await _dbContext.Categories.AnyAsync(x => x.Name.ToLower().Equals(category.Name.ToLower()));

            if (isExist)
            {
                ModelState.AddModelError("Name", "Bu adda kateqoriya movcuddur");
                return View();
            }


            await _dbContext.Categories.AddAsync(category);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null) return NotFound();

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null) return NotFound();

            var category = await _dbContext.Categories.FindAsync(id);

            if (category == null) return NotFound();

            _dbContext.Categories.Remove(category);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

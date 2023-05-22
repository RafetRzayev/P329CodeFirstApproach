using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.ViewModels;

namespace P329CodeFirstApproach.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var categories = _dbContext.Categories.ToList();
            var products = _dbContext.Products.ToList();

            var viewModel = new ProductViewModel
            {
                Categories = categories,
                Products = products
            };

            return View(viewModel);
        }

        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            var product = _dbContext.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);

            return View(product);
        }
    }
}

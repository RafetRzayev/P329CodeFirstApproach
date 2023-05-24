using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.ViewModels;

namespace P329CodeFirstApproach.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly int _productCount;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _productCount = _dbContext.Products.Count();
        }

        public IActionResult Index()
        {
            ViewBag.ProductCount = _productCount;   

            var categories = _dbContext.Categories.ToList();
            var products = _dbContext.Products.Take(4).ToList();

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

        public IActionResult LoadProducts(int skip)
        {
            if (skip >= _productCount) return BadRequest();

            var products = _dbContext.Products.Include(x => x.Category).Skip(skip).Take(4).ToList();

            return PartialView("_ProductPartial", products);
        }
    }
}

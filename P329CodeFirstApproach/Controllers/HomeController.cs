using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.DataAccessLayer.Entities;
using P329CodeFirstApproach.ViewModels;

namespace P329CodeFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext appDbContext)
        {
            _dbContext = appDbContext;
        }

        public IActionResult Index()
        {
            var sliderImage = _dbContext.SliderImages.FirstOrDefault();
            var sliderBgs = _dbContext.SliderBgs.ToList();
            var categories = _dbContext.Categories.ToList();
            var products = _dbContext.Products.Include(x => x.Category).ToList();

            var viewModel = new HomeViewModel
            {
                SliderImage = sliderImage,
                SliderBg = sliderBgs,
                Categories = categories,
                Products = products
            };

            return View(viewModel);
        }

        public IActionResult Search(string searchedProduct)
        {
            if (string.IsNullOrEmpty(searchedProduct)) return NoContent();

            var products = _dbContext.Products.Where(x => x.Name.Contains(searchedProduct)).ToList();

            return PartialView("_SearchedProductPartial", products);
        }
    }
}

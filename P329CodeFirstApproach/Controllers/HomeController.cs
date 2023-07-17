using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using P329CodeFirstApproach.Data;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.DataAccessLayer.Entities;
using P329CodeFirstApproach.Services;
using P329CodeFirstApproach.SessionExtensions;
using P329CodeFirstApproach.ViewModels;

namespace P329CodeFirstApproach.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IMailService _mailService;
        public HomeController(AppDbContext appDbContext, IMailService mailService)
        {
            _dbContext = appDbContext;
            _mailService = mailService;
        }

        public IActionResult Index()
        {
            HttpContext.Session.SetString("p329", "hello");
            Response.Cookies.Append("a", "test1",new CookieOptions { Expires = DateTimeOffset.Now.AddDays(1)});
            Response.Cookies.Append("b", "test2");

            if (HttpContext.Session.GetInt32("IsVisited") == null)
            {
                HttpContext.Session.SetInt32("IsVisited", 1);
                var visitor = _dbContext.Visitors.FirstOrDefault();
                visitor.Count++;
                _dbContext.SaveChanges();
            }

            

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

            HttpContext.Session.SetAsJson("products", products);

            return View(viewModel);
        }


        public IActionResult Basket()
        {
            var json = Request.Cookies["basket"];

            List<BasketViewModel> basketViewModels;

            if (json == null) basketViewModels = new List<BasketViewModel>();
            else basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(json);

            return Json(basketViewModels);
        }


        public IActionResult AddToBasket(int? id)
        {
            if (id == null) return NotFound();

            var product = _dbContext.Products.Find(id);

            var json = Request.Cookies["basket"];
            List<BasketViewModel> basketViewModels;

            if (json == null) basketViewModels = new List<BasketViewModel>();
            else basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(json);

            if (basketViewModels == null)
            {
                basketViewModels = new List<BasketViewModel>();
                basketViewModels.Add(new BasketViewModel
                {
                    ProductId = product.Id,
                    Name = product.Name,
                    Price = product.Price
                });
            }
            else
            {
                var existBasketViewModel = basketViewModels.SingleOrDefault(x => x.ProductId == product.Id);

                if (existBasketViewModel == null)
                {
                    basketViewModels.Add(new BasketViewModel
                    {
                        ProductId = product.Id,
                        Name = product.Name,
                        Price = product.Price
                    });
                }
                else
                {
                    existBasketViewModel.Count++;
                }
            }

            json = JsonConvert.SerializeObject(basketViewModels);

            Response.Cookies.Append("basket", json);

            return PartialView("_BasketPartial",basketViewModels);
        }

        public IActionResult Search(string searchedProduct)
        {
            if (string.IsNullOrEmpty(searchedProduct)) return NoContent();

            var products = _dbContext.Products.Where(x => x.Name.Contains(searchedProduct)).ToList();

            return PartialView("_SearchedProductPartial", products);
        }
    }
}

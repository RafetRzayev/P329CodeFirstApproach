using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.DataAccessLayer;

namespace P329CodeFirstApproach.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ProductViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var products = _dbContext.Products.Include(x => x.Category).Take(8).ToList();

            return View(products);
        }
    }
}

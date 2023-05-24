using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.DataAccessLayer;

namespace P329CodeFirstApproach.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IViewComponentResult Invoke()
        {
            var footer = _dbContext.Footer.Include(x => x.SocialNetworks).FirstOrDefault();

            return View(footer);
          //var footer = _dbContext.Footer.Include(x=>x.SocialNetworks).SingleOrDefault();
        }
    }
}

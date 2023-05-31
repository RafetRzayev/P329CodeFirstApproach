using Microsoft.AspNetCore.Mvc;

namespace P329CodeFirstApproach.Areas.AdminPanel.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

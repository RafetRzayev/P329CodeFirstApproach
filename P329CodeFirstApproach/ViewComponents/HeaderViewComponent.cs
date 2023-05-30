using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using P329CodeFirstApproach.ViewModels;

namespace P329CodeFirstApproach.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var json = Request.Cookies["basket"];

            List<BasketViewModel> basketViewModels;

            if (json == null) basketViewModels = new List<BasketViewModel>();
            else basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(json);

            return View(basketViewModels);
        }
    }
}

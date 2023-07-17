using Microsoft.AspNetCore.Mvc.Rendering;

namespace P329CodeFirstApproach.Areas.AdminPanel.Models
{
    public class CreateProductViewModel
    {
        public string Name { get; set; }
        public double Price { get; set; }

        public List<SelectListItem> CategoryListItems { get; set; } =new List<SelectListItem>();

        public int CategoryId { get; set; } 
    }
}

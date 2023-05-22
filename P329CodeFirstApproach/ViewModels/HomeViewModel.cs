using P329CodeFirstApproach.DataAccessLayer.Entities;

namespace P329CodeFirstApproach.ViewModels
{
    public class HomeViewModel
    {
        public SliderImage SliderImage { get; set; }
        public List<SliderBg> SliderBg { get; set; }   
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }
    }
}

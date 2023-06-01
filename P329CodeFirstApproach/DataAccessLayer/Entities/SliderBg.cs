using System.ComponentModel.DataAnnotations.Schema;

namespace P329CodeFirstApproach.DataAccessLayer.Entities
{
    public class SliderBg
    {
        public int Id { get; set; }
        public string? ImageUrl { get; set; }

        [NotMapped]
        public IFormFile Photo { get; set; }
    }
}

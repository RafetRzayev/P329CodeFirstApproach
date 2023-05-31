using System.ComponentModel.DataAnnotations;

namespace P329CodeFirstApproach.DataAccessLayer.Entities
{
    public class Category
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public List<Product>? Products { get; set; }
    }
}

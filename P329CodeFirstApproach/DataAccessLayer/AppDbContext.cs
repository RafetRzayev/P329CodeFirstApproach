using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.DataAccessLayer.Entities;

namespace P329CodeFirstApproach.DataAccessLayer
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
                
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<SliderBg> SliderBgs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Footer> Footer { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<Visitor> Visitors { get; set; }    
        public DbSet<TestEntity> TestEntities { get; set; }
    }
}

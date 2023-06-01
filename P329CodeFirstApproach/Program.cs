using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.Areas.AdminPanel.Data;
using P329CodeFirstApproach.DataAccessLayer;

namespace P329CodeFirstApproach
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc();
            builder.Services.AddSession(option =>
            {
                option.Cookie.Name = "MySession";
                option.IdleTimeout = TimeSpan.FromSeconds(40);
            });

            Constants.ImagePath = Path.Combine(builder.Environment.WebRootPath, "img");

            var connectinString = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectinString1 = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");
            var test = builder.Configuration.GetValue<string>("Test:Inner");

            builder.Services.AddDbContext<AppDbContext>(builder =>
            {
                builder.UseSqlServer(connectinString);
            });

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                            name: "areas",
                            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute("default","{controller=home}/{action=index}/{id?}"); 
            });

            app.Run();
        }
    }
}
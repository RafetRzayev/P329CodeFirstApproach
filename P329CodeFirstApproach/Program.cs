using MailKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.Areas.AdminPanel.Data;
using P329CodeFirstApproach.Data;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.Services;
using IMailService = P329CodeFirstApproach.Services.IMailService;

namespace P329CodeFirstApproach
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddMvc();
            builder.Services.AddSession(option =>
            {
                option.Cookie.Name = "MySession";
                option.IdleTimeout = TimeSpan.FromDays(1);
            });

            Constants.ImagePath = Path.Combine(builder.Environment.WebRootPath, "img");

            var connectinString = builder.Configuration.GetConnectionString("DefaultConnection");
            var connectinString1 = builder.Configuration.GetSection("ConnectionStrings").GetValue<string>("DefaultConnection");
            var test = builder.Configuration.GetValue<string>("Test:Inner");

            builder.Services.AddDbContext<AppDbContext>(builder =>
            {
                builder.UseSqlServer(connectinString,builder=>builder.MigrationsAssembly("P329CodeFirstApproach"));
                
            });

            builder.Services.Configure<MailSetting>(builder.Configuration.GetSection("MailSettings"));

            builder.Services.AddTransient<IMailService, GmailManager>();

            builder.Services.AddIdentity<User, IdentityRole>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;

                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedPhoneNumber = false;
                options.SignIn.RequireConfirmedEmail= false;

                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(2);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders().AddErrorDescriber<LocalizeIdentityError>();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var dataInitializer = new DataInitializer(userManager, roleManager, dbContext);
                await dataInitializer.SeedData();
            };

            app.UseStaticFiles();
            app.UseSession();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                            name: "areas",
                            pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute("default","{controller=home}/{action=index}/{id?}"); 
            });

            await app.RunAsync();
        }
    }
}
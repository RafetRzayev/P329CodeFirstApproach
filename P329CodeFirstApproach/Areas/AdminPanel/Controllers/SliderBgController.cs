using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.Areas.AdminPanel.Data;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.DataAccessLayer.Entities;

namespace P329CodeFirstApproach.Areas.AdminPanel.Controllers
{
    public class SliderBgController : AdminController
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;
        public SliderBgController(AppDbContext dbContext, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _environment = environment;
            _configuration = configuration;
        }

        public async Task<IActionResult> Index()
        {
            var sliderBgs = await _dbContext.SliderBgs.ToListAsync();

            return View(sliderBgs);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderBg sliderBg)
        {
            if (!ModelState.IsValid)
                return View();

            if (!sliderBg.Photo.ContentType.Contains("image"))
            {
                ModelState.AddModelError("photo", "sekil secmelisen");
                return View();
            }

            if (sliderBg.Photo.Length > 1024 * 1024)
            {
                ModelState.AddModelError("photo", "1mb-dan cox olmamalidir");
                return View();
            }
            
            var fileName = $"{Guid.NewGuid()}-{sliderBg.Photo.FileName}";

            var path = Path.Combine(Constants.ImagePath, fileName );

            var fs = new FileStream(path, FileMode.CreateNew);

            await sliderBg.Photo.CopyToAsync(fs);
           
            fs.Close();
           
            sliderBg.ImageUrl = fileName;

            await _dbContext.SliderBgs.AddAsync(sliderBg);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

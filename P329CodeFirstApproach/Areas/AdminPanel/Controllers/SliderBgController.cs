using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P329CodeFirstApproach.Areas.AdminPanel.Data;
using P329CodeFirstApproach.DataAccessLayer;
using P329CodeFirstApproach.DataAccessLayer.Entities;
using Files = System.IO.File;

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

            foreach (var photo in sliderBg.Photos)
            {
                if (!photo.IsImage())
                {
                    ModelState.AddModelError("photo", "sekil secmelisen");
                    return View();
                }

                if (!photo.IsAllowedSize(1))
                {
                    ModelState.AddModelError("photo", "1mb-dan cox olmamalidir");
                    return View();
                }     
            }

            foreach (var photo in sliderBg.Photos)
            {
                var fileName = await photo.GenerateFile(Constants.ImagePath);

                sliderBg = new SliderBg
                {
                    ImageUrl = fileName
                };

                await _dbContext.SliderBgs.AddAsync(sliderBg);
                await _dbContext.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();

            var sliderBg = await _dbContext.SliderBgs.FindAsync(id);

            if (sliderBg == null) return NotFound();

            return View(sliderBg);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SliderBg sliderBg)
        {
            if (id == null) return NotFound();

            if (id != sliderBg.Id) return BadRequest();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existSliderBg = await _dbContext.SliderBgs.FindAsync(id);

            if (existSliderBg == null) return NotFound();


            if (!sliderBg.Photo.IsImage())
            {
                ModelState.AddModelError("photo", "sekil secmelisen");
                return View(existSliderBg);
            }

            if (!sliderBg.Photo.IsAllowedSize(1))
            {
                ModelState.AddModelError("photo", "1mb-dan cox olmamalidir");
                return View(existSliderBg);
            }

            var fileName = await sliderBg.Photo.GenerateFile(Constants.ImagePath);

            var path = Path.Combine(Constants.ImagePath, existSliderBg.ImageUrl);

            if (Files.Exists(path))
            {
                Files.Delete(path);
            }

            existSliderBg.ImageUrl = fileName;

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var sliderBg = await _dbContext.SliderBgs.FindAsync(id);

            if (sliderBg == null) return NotFound();

            var path = Path.Combine(Constants.ImagePath, sliderBg.ImageUrl);

            if (Files.Exists(path))
            {
                Files.Delete(path);
            }

            _dbContext.SliderBgs.Remove(sliderBg);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}

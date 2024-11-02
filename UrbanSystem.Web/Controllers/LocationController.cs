using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Web.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var locations = await _context.Locations
                .Select(l => new LocationInfoViewModel()
                {
                    Id = l.Id.ToString(),
                    CityName = l.CityName,
                    CityPicture = l.CityPicture
                })
                .OrderBy(l => l.CityName)
                .ToListAsync();

            return View(locations);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(LocationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            Location location = new Location()
            {
                CityName = model.CityName,
                StreetName = model.StreetName,
                CityPicture = model.CityPicture
            };

            await _context.Locations.AddAsync(location);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }
    }
}

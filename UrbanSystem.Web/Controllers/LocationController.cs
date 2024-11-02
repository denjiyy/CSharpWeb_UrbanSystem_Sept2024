using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
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

        public async Task<IActionResult> Index()
        {
            var locations = await _context.Locations
                .Select(l => new LocationInfoViewModel()
                {
                    Id = l.Id.ToString(),
                    CityName = l.CityName,
                    StreetName = l.StreetName
                })
                .ToListAsync();

            return View(locations);
        }
    }
}

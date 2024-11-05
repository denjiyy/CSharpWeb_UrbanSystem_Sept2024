using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.SuggestionsLocations;

namespace UrbanSystem.Web.Controllers
{
    public class LocationController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public LocationController(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var locations = await _context.Locations
                .Select(l => new LocationDetailsViewModel()
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

        [HttpGet]
        public async Task<IActionResult> Details(string? id)
        {
            if (!Guid.TryParse(id, out Guid locationGuid))
            {
                TempData["ErrorMessage"] = "Invalid location ID.";
                return RedirectToAction(nameof(All));
            }

            var location = await _context.Locations
                .Include(l => l.SuggestionsLocations)
                    .ThenInclude(sl => sl.Suggestion)
                .FirstOrDefaultAsync(l => l.Id == locationGuid);

            if (location == null)
            {
                TempData["ErrorMessage"] = "Location not found.";
                return RedirectToAction(nameof(All));
            }

            var details = new LocationDetailsViewModel
            {
                Id = location.Id.ToString(),
                CityName = location.CityName,
                StreetName = location.StreetName,
                CityPicture = location.CityPicture,
                Suggestions = location.SuggestionsLocations
                    .Select(sl => new SuggestionLocationViewModel
                    {
                        Id = sl.Suggestion.Id.ToString(),
                        Title = sl.Suggestion.Title,
                        UploadedOn = sl.Suggestion.UploadedOn.ToString("dd/MM/yyyy")
                    })
                    .OrderBy(s => s.UploadedOn)
                    .ToList()
            };

            return View(details);
        }
    }
}

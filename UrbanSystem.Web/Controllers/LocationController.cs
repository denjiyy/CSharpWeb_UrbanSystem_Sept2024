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

        public LocationController(ApplicationDbContext context)
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
            Guid locationGuid = Guid.Empty;
            var isGuidValid = this.IsGuidIdValid(id, ref locationGuid);

            if (!isGuidValid)
            {
                return RedirectToAction(nameof(All));
            }

            Location location = await _context
                .Locations
                .FirstOrDefaultAsync(l => l.Id == locationGuid)!;

            if (location == null)
            {
                return RedirectToAction(nameof(All));
            }

            LocationDetailsViewModel details = new LocationDetailsViewModel()
            {
                CityName = location.CityName,
                StreetName = location.StreetName,
                CityPicture = location.CityPicture,
                Suggestions = location.SuggestionsLocations
                     .Select(sl => new SuggestionLocationViewModel()
                     {
                         Id = sl.Suggestion.Id.ToString(),
                         Title = sl.Suggestion.Title,
                         UploadedOn = sl.Suggestion.UploadedOn.ToString()
                     }).ToList()
            };

            return View(details);
        }
    }
}

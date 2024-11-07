using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.SuggestionsLocations;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class LocationController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly ILocationService _locationService;

        public LocationController(ApplicationDbContext context, ILocationService locationService) : base(context)
        {
            _context = context;
            _locationService = locationService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var locations = await _locationService.GetAllOrderedByNameAsync();

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

            await _locationService.AddLocationAsync(model);

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var details = await _locationService.GetLocationDetailsByIdAsync(id);

            return View(details);
        }
    }
}

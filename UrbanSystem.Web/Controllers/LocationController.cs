using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class LocationController : BaseController
    {
        private readonly ILocationService _locationService;
        private readonly ILogger<LocationController> _logger;

        public LocationController(ILocationService locationService, ILogger<LocationController> logger) : base()
        {
            _locationService = locationService ?? throw new ArgumentNullException(nameof(locationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                var locations = await _locationService.GetAllOrderedByNameAsync();
                return View(locations);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all locations");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View(new LocationFormViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] LocationFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await _locationService.AddLocationAsync(model);
                _logger.LogInformation("New location added successfully");
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new location");
                ModelState.AddModelError(string.Empty, "An error occurred while adding the location. Please try again.");
                return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid location ID.");
            }

            try
            {
                var details = await _locationService.GetLocationDetailsByIdAsync(id);

                if (details == null)
                {
                    _logger.LogWarning("Location not found: {LocationId}", id);
                    return NotFound("The requested location could not be found.");
                }

                return View(details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching location details for ID: {LocationId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
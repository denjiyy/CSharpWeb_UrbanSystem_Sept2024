﻿using Microsoft.AspNetCore.Authorization;
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
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService) : base()
        {
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
        public async Task<IActionResult> Details(string? id)
        {
            Guid locationGuid = Guid.Empty;
            // temporary patch
            bool isIdValid = IsGuidIdValid(id?.ToLower(), ref locationGuid);
            if (!isIdValid)
            {
                return RedirectToAction(nameof(All));
            }

            var details = await _locationService.GetLocationDetailsByIdAsync(locationGuid);

            if (details == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(details);
        }
    }
}

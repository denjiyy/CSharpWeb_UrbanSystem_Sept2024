using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    public class SuggestionController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public SuggestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var suggestions = await _context.Suggestions
                .Select(s => new SuggestionIndexViewModel
                {
                    Id = s.Id.ToString(),
                    Title = s.Title,
                    Category = s.Category,
                    AttachmentUrl = s.AttachmentUrl,
                    Description = s.Description,
                    UploadedOn = s.UploadedOn.ToString("dd/MM/yyyy"),
                    Status = s.Status,
                    Upvotes = s.Upvotes,
                    Downvotes = s.Downvotes,
                    Priority = s.Priority,
                    LocationNames = s.SuggestionsLocations
                        .Select(sl => sl.Location.CityName)
                        .ToList()
                })
                .ToListAsync();

            return View(suggestions);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var cities = await _context.Locations
                .Select(l => new CityOption
                {
                    Value = l.Id.ToString(),
                    Text = l.CityName
                })
                .ToListAsync();

            var viewModel = new SuggestionFormViewModel
            {
                Cities = cities
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SuggestionFormViewModel suggestionModel)
        {
            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == suggestionModel.CityId);

            var suggestion = new Suggestion
            {
                Title = suggestionModel.Title,
                Category = suggestionModel.Category,
                AttachmentUrl = suggestionModel.AttachmentUrl,
                Description = suggestionModel.Description,
                Priority = suggestionModel.Priority,
                Status = suggestionModel.Status
            };

            await _context.Suggestions.AddAsync(suggestion);
            await _context.SaveChangesAsync();

            if (location != null)
            {
                var suggestionLocation = new SuggestionLocation
                {
                    SuggestionId = suggestion.Id,
                    LocationId = location.Id
                };

                await _context.SuggestionsLocations.AddAsync(suggestionLocation);
                await _context.SaveChangesAsync();
            }
            else
            {
                ModelState.AddModelError("", "The specified location does not exist.");
                return View(suggestionModel);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                TempData["ErrorMessage"] = "Invalid suggestion ID.";
                return RedirectToAction(nameof(All));
            }

            var suggestion = await _context.Suggestions
                .Include(s => s.SuggestionsLocations)
                    .ThenInclude(sl => sl.Location)
                .FirstOrDefaultAsync(s => s.Id == suggestionId);

            if (suggestion == null)
            {
                TempData["ErrorMessage"] = "Suggestion not found.";
                return RedirectToAction(nameof(All));
            }

            var viewModel = new SuggestionIndexViewModel
            {
                Id = suggestion.Id.ToString(),
                Title = suggestion.Title,
                Category = suggestion.Category,
                AttachmentUrl = suggestion.AttachmentUrl,
                Description = suggestion.Description,
                UploadedOn = suggestion.UploadedOn.ToString("dd/MM/yyyy"),
                Status = suggestion.Status,
                Upvotes = suggestion.Upvotes,
                Downvotes = suggestion.Downvotes,
                Priority = suggestion.Priority,
                LocationNames = suggestion.SuggestionsLocations
                    .Select(sl => sl.Location.CityName)
                    .ToList()
            };

            return View(viewModel);
        }

    }
}

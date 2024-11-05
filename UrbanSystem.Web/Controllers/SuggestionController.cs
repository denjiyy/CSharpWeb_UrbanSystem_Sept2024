using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    public class SuggestionController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuggestionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : base(context)
        {
            _context = context;
            _userManager = userManager;
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
            var viewModel = await LoadLocations();

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SuggestionFormViewModel suggestionModel)
        {
            string? userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            if (!Guid.TryParse(userId, out Guid parsedUserId))
            {
                return NotFound("User ID is not valid");
            }

            var user = await _context.Users.FindAsync(parsedUserId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var location = await _context.Locations
                .FirstOrDefaultAsync(l => l.Id == suggestionModel.CityId);

            if (location == null)
            {
                ModelState.AddModelError("", "The specified location does not exist.");
                return View(suggestionModel);
            }

            var suggestion = new Suggestion
            {
                Title = suggestionModel.Title,
                Category = suggestionModel.Category,
                AttachmentUrl = suggestionModel.AttachmentUrl,
                Description = suggestionModel.Description,
                Priority = suggestionModel.Priority,
                Status = suggestionModel.Status
            };

            _context.Suggestions.Add(suggestion);
            await _context.SaveChangesAsync();

            var applicationUserSuggestion = new ApplicationUserSuggestion
            {
                ApplicationUserId = user.Id,
                SuggestionId = suggestion.Id
            };

            _context.UsersSuggestions.Add(applicationUserSuggestion);

            var suggestionLocation = new SuggestionLocation
            {
                SuggestionId = suggestion.Id,
                LocationId = location.Id
            };

            _context.SuggestionsLocations.Add(suggestionLocation);

            await _context.SaveChangesAsync();

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

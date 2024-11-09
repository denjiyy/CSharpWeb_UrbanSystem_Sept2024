using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Security.Claims;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class SuggestionController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISuggestionService _suggestionService;

        public SuggestionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ISuggestionService suggestionService) : base(context)
        {
            _context = context;
            _userManager = userManager;
            _suggestionService = suggestionService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var suggestions = await _suggestionService
                .GetAllSuggestionsAsync();

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
            var userId = _userManager.GetUserId(User);
            var isSuccessful = await _suggestionService.AddSuggestionAsync(suggestionModel, userId);

            if (!isSuccessful)
            {
                await LoadLocations();
                return View(suggestionModel);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [AllowAnonymous]
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

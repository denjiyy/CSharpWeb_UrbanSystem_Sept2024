using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Graph.Models;
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

            var suggestion = await _suggestionService.GetSuggestionDetailsAsync(suggestionId);

            if (suggestion == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(suggestion);
        }
    }
}

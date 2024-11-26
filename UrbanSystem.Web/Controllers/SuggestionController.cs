using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class SuggestionController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISuggestionService _suggestionService;
        private readonly ILogger<SuggestionController> _logger;

        public SuggestionController(
            IBaseService baseService,
            UserManager<ApplicationUser> userManager,
            ISuggestionService suggestionService,
            ILogger<SuggestionController> logger) : base(baseService)
        {
            _userManager = userManager;
            _suggestionService = suggestionService;
            _logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var suggestions = await _suggestionService.GetAllSuggestionsAsync();
            return View(suggestions);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = await _suggestionService.GetSuggestionFormViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] SuggestionFormViewModel suggestionModel)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _suggestionService.AddSuggestionAsync(suggestionModel, userId);

            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(result.ViewModel);
            }

            _logger.LogInformation($"User {userId} added a new suggestion.");
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid suggestion ID.");
            }

            var userId = _userManager.GetUserId(User);
            var result = await _suggestionService.GetSuggestionDetailsAsync(id, userId);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning($"Failed to retrieve suggestion details for ID: {id}. Error: {result.ErrorMessage}");
                TempData["ErrorMessage"] = "The requested suggestion could not be found.";
                return RedirectToAction(nameof(All));
            }

            return View(result.Suggestion);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string suggestionId, [FromForm] string content)
        {
            if (string.IsNullOrEmpty(suggestionId) || string.IsNullOrEmpty(content))
            {
                return BadRequest("Invalid suggestion ID or comment content.");
            }

            var userId = _userManager.GetUserId(User);
            var result = await _suggestionService.AddCommentAsync(suggestionId, content, userId);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning($"Failed to add comment to suggestion {suggestionId}. Error: {result.ErrorMessage}");
                return BadRequest("Failed to add comment. Please try again.");
            }

            return RedirectToAction(nameof(Details), new { id = suggestionId });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid suggestion ID.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _suggestionService.GetSuggestionForEditAsync(id, currentUser);

            if (!result.IsSuccessful)
            {
                _logger.LogWarning($"Failed to retrieve suggestion for edit. ID: {id}, User: {currentUser.Id}. Error: {result.ErrorMessage}");
                TempData["ErrorMessage"] = "You do not have permission to edit this suggestion.";
                return RedirectToAction(nameof(All));
            }

            return View(result.ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, [FromForm] SuggestionFormViewModel model)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid suggestion ID.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _suggestionService.UpdateSuggestionAsync(id, model, currentUser.Id.ToString());

            if (!result.IsSuccessful)
            {
                _logger.LogWarning($"Failed to update suggestion. ID: {id}, User: {currentUser.Id}. Error: {result.ErrorMessage}");
                TempData["ErrorMessage"] = "Failed to update the suggestion. Please try again.";
                return View(model);
            }

            _logger.LogInformation($"User {currentUser.Id} updated suggestion {id}.");
            TempData["SuccessMessage"] = "Suggestion updated successfully.";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid suggestion ID.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _suggestionService.GetSuggestionForDeleteConfirmationAsync(id, currentUser.Id.ToString());

            if (!result.IsSuccessful)
            {
                _logger.LogWarning($"Failed to retrieve suggestion for delete confirmation. ID: {id}, User: {currentUser.Id}. Error: {result.ErrorMessage}");
                TempData["ErrorMessage"] = "You do not have permission to delete this suggestion.";
                return RedirectToAction(nameof(All));
            }

            return View(result.ViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest("Invalid suggestion ID.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _suggestionService.DeleteSuggestionAsync(id, currentUser.Id.ToString());

            if (!result.IsSuccessful)
            {
                _logger.LogWarning($"Failed to delete suggestion. ID: {id}, User: {currentUser.Id}. Error: {result.ErrorMessage}");
                TempData["ErrorMessage"] = "Failed to delete the suggestion. Please try again.";
                return RedirectToAction(nameof(Details), new { id = id });
            }

            _logger.LogInformation($"User {currentUser.Id} deleted suggestion {id}.");
            TempData["SuccessMessage"] = "Suggestion deleted successfully.";
            return RedirectToAction(nameof(All));
        }
    }
}


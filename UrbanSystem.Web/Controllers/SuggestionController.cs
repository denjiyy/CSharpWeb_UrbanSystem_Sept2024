using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class SuggestionController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISuggestionService _suggestionService;

        public SuggestionController(IBaseService baseService, UserManager<ApplicationUser> userManager, ISuggestionService suggestionService) : base(baseService)
        {
            _userManager = userManager;
            _suggestionService = suggestionService;
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
        public async Task<IActionResult> Add(SuggestionFormViewModel suggestionModel)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _suggestionService.AddSuggestionAsync(suggestionModel, userId);

            if (!result.IsSuccessful)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                return View(result.ViewModel);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(string id)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _suggestionService.GetSuggestionDetailsAsync(id, userId);

            if (!result.IsSuccessful)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction(nameof(All));
            }

            return View(result.Suggestion);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string suggestionId, string content)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _suggestionService.AddCommentAsync(suggestionId, content, userId);

            if (!result.IsSuccessful)
            {
                return BadRequest(result.ErrorMessage);
            }

            return RedirectToAction(nameof(Details), new { id = suggestionId });
        }

        [HttpPost]
        public async Task<IActionResult> VoteComment(string commentId, bool isUpvote)
        {
            var userId = _userManager.GetUserId(User);
            var result = await _suggestionService.VoteCommentAsync(commentId, userId, isUpvote);

            if (!result.IsSuccessful)
            {
                return NotFound(result.ErrorMessage);
            }

            return Json(new { upvotes = result.Comment.Upvotes, downvotes = result.Comment.Downvotes });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _suggestionService.GetSuggestionForEditAsync(id, currentUser);

            if (!result.IsSuccessful)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction(nameof(All));
            }

            return View(result.ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SuggestionFormViewModel model)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _suggestionService.UpdateSuggestionAsync(id, model, currentUser.Id.ToString());

            if (!result.IsSuccessful)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return View(model);
            }

            TempData["SuccessMessage"] = "Suggestion updated successfully.";
            return RedirectToAction(nameof(Details), new { id = id });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _suggestionService.GetSuggestionForDeleteConfirmationAsync(id, currentUser.Id.ToString());

            if (!result.IsSuccessful)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction(nameof(All));
            }

            return View(result.ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var result = await _suggestionService.DeleteSuggestionAsync(id, currentUser.Id.ToString());

            if (!result.IsSuccessful)
            {
                TempData["ErrorMessage"] = result.ErrorMessage;
                return RedirectToAction(nameof(Details), new { id = id });
            }

            TempData["SuccessMessage"] = "Suggestion deleted successfully.";
            return RedirectToAction(nameof(All));
        }
    }
}
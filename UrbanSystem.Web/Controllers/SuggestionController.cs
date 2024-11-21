using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISuggestionService _suggestionService;
        private new readonly IBaseService _baseService;

        public SuggestionController(IBaseService baseService, UserManager<ApplicationUser> userManager, ISuggestionService suggestionService) : base(baseService)
        {
            _userManager = userManager;
            _suggestionService = suggestionService;
            _baseService = baseService;
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
            var cities = await CityList();

            var viewModel = new SuggestionFormViewModel
            {
                Cities = cities
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SuggestionFormViewModel suggestionModel)
        {
            var userId = _userManager.GetUserId(User);
            var isSuccessful = await _suggestionService.AddSuggestionAsync(suggestionModel, userId);

            if (!isSuccessful)
            {
                suggestionModel.Cities = await CityList();
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

            var userId = _userManager.GetUserId(User);

            var suggestion = await _suggestionService.GetSuggestionDetailsAsync(suggestionId, userId);

            if (suggestion == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(suggestion);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(string suggestionId, string content)
        {
            if (!Guid.TryParse(suggestionId, out Guid parsedSuggestionId))
            {
                return BadRequest("Invalid suggestion ID.");
            }

            var userId = _userManager.GetUserId(User);

            var result = await _suggestionService.AddCommentAsync(parsedSuggestionId, content, userId);

            if (!result)
            {
                return BadRequest("Failed to add comment.");
            }

            return RedirectToAction(nameof(Details), new { id = suggestionId });
        }

        [HttpPost]
        public async Task<IActionResult> VoteComment(string commentId, bool isUpvote)
        {
            if (!Guid.TryParse(commentId, out Guid parsedCommentId))
            {
                return BadRequest("Invalid comment ID.");
            }

            var userId = _userManager.GetUserId(User);

            var result = await _suggestionService.VoteCommentAsync(parsedCommentId, userId, isUpvote);

            if (!result)
            {
                return NotFound();
            }

            var updatedComment = await _suggestionService.GetCommentAsync(parsedCommentId);

            if (updatedComment == null)
            {
                return NotFound();
            }

            return Json(new { upvotes = updatedComment.Upvotes, downvotes = updatedComment.Downvotes });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                TempData["ErrorMessage"] = "Invalid suggestion ID.";
                return RedirectToAction(nameof(All));
            }

            ApplicationUser? user = _userManager.GetUserAsync(User).Result; 

            var suggestion = await _suggestionService.GetSuggestionForEditAsync(suggestionId, user);

            if (suggestion == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null || Guid.Parse(suggestion.UserId) != currentUser.Id)
            {
                return Forbid();
            }

            return View(suggestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, SuggestionFormViewModel model)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                TempData["ErrorMessage"] = "Invalid suggestion ID.";
                return RedirectToAction(nameof(All));
            }

            

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var result = await _suggestionService.UpdateSuggestionAsync(suggestionId, model, currentUser.Id.ToString());

            if (!result)
            {
                TempData["ErrorMessage"] = "You are not authorized to edit this suggestion or the suggestion was not found.";
                return RedirectToAction(nameof(All));
            }

            TempData["SuccessMessage"] = "Suggestion updated successfully.";
            return RedirectToAction(nameof(Details), new { id = suggestionId });
        }

        [HttpGet]
        public async Task<IActionResult> ConfirmDelete(string id)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                TempData["ErrorMessage"] = "Invalid suggestion ID.";
                return RedirectToAction(nameof(All));
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var suggestion = await _suggestionService.GetSuggestionForDeleteConfirmationAsync(suggestionId, currentUser.Id.ToString());

            if (suggestion == null)
            {
                TempData["ErrorMessage"] = "Suggestion not found or you're not authorized to delete it.";
                return RedirectToAction(nameof(All));
            }

            return View(suggestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                TempData["ErrorMessage"] = "Invalid suggestion ID.";
                return RedirectToAction(nameof(All));
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            var result = await _suggestionService.DeleteSuggestionAsync(suggestionId, currentUser.Id.ToString());

            if (!result)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the suggestion.";
                return RedirectToAction(nameof(Details), new { id = suggestionId });
            }

            TempData["SuccessMessage"] = "Suggestion deleted successfully.";
            return RedirectToAction(nameof(All));
        }
    }
}

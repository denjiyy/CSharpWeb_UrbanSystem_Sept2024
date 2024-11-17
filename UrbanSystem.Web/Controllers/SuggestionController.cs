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

            var suggestion = await _suggestionService.GetSuggestionDetailsAsync(suggestionId);

            if (suggestion == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(suggestion);
        }

        [HttpPost]
        [Authorize]
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
        [Authorize]
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
    }
}

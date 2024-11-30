using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class MySuggestionController : BaseController
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMySuggestionService _mySuggestionService;

        public MySuggestionController(UserManager<ApplicationUser> userManager, IMySuggestionService mySuggestionService) : base()
        {
            _userManager = userManager;
            _mySuggestionService = mySuggestionService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            string? userId = _userManager.GetUserId(User);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToPage("/Identity/Account/Login");
            }

            var mySuggestions = await _mySuggestionService.GetAllSuggestionsForLoggedInUser(userId);

            return View(mySuggestions);
        }
    }
}

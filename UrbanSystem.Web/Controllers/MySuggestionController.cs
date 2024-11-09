using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Suggestions;

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
            var mySuggestions = await _mySuggestionService.GetAllSuggestionsForLoggedInUser(userId);

            return View(mySuggestions);
        }
    }
}

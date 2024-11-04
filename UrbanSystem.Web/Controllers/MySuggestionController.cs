using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class MySuggestionController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MySuggestionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            string? userId = _userManager.GetUserId(User);

            IEnumerable<MySuggestionsViewModel> mySuggestions = await _context
                .UsersSuggestions
                .Include(us => us.Suggestion)
                .Where(us => us.ApplicationUserId.ToString().ToLower() == userId.ToLower())
                .Select(us => new MySuggestionsViewModel()
                {
                    Id  = us.SuggestionId.ToString(),
                    Title = us.Suggestion.Title,
                    Category = us.Suggestion.Category,
                    UploadedOn = us.Suggestion.UploadedOn.ToString("dd/MM/yyyy"),
                    AttachmentUrl = us.Suggestion.AttachmentUrl,
                    Upvotes = us.Suggestion.Upvotes.ToString(),
                    Downvotes = us.Suggestion.Downvotes.ToString()
                })
                .OrderBy(us => us.UploadedOn)
                .ToListAsync();

            return View(mySuggestions);
        }
    }
}

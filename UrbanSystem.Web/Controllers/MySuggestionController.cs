using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class MySuggestionController : BaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IRepository<Suggestion, Guid> _repository;

        public MySuggestionController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, IRepository<Suggestion, Guid> repository) : base(context)
        {
            _userManager = userManager;
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            string? userId = _userManager.GetUserId(User);

            var suggestions = await _context
                .UsersSuggestions
                .Include(us => us.Suggestion)
                .Where(us => us.ApplicationUserId.ToString().ToLower() == userId!.ToLower())
                .Select(us => new
                {
                    us.SuggestionId,
                    us.Suggestion.Title,
                    us.Suggestion.Category,
                    us.Suggestion.UploadedOn,
                    us.Suggestion.AttachmentUrl,
                    us.Suggestion.Upvotes,
                    us.Suggestion.Downvotes
                })
                .OrderBy(us => us.UploadedOn)
                .ToListAsync();

            var mySuggestions = suggestions.Select(us => new MySuggestionsViewModel()
            {
                Id = us.SuggestionId.ToString(),
                Title = us.Title,
                Category = us.Category,
                UploadedOn = us.UploadedOn.ToString("dd/MM/yyyy"),
                AttachmentUrl = us.AttachmentUrl,
                Upvotes = us.Upvotes.ToString(),
                Downvotes = us.Downvotes.ToString()
            });

            return View(mySuggestions);
        }
    }
}

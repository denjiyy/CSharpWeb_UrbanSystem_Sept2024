using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
    public class SuggestionController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SuggestionController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            var suggestions = await _context.Suggestions
                .ToListAsync();

            return View(suggestions);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(SuggestionFormViewModel suggestionModel)
        {
            if (!ModelState.IsValid)
            {
                return View(suggestionModel);
            }

            Suggestion suggestion = new Suggestion
            {
                Title = suggestionModel.Title,
                Category = suggestionModel.Category,
                AttachmentUrl = suggestionModel.AttachmentUrl,
                Description = suggestionModel.Description,
                Priority = suggestionModel.Priority,
                Status = suggestionModel.Status
            };

            await _context.Suggestions.AddAsync(suggestion);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            bool isIdValid = Guid.TryParse(id, out Guid guidId);

            if (!isIdValid)
            {
                return RedirectToAction(nameof(All));
            }

            var suggestion = await _context
                .Suggestions
                .FirstOrDefaultAsync(m => m.Id == guidId);

            if (suggestion == null)
            {
                return RedirectToAction(nameof(All));
            }

            return View(suggestion);
        }
    }
}

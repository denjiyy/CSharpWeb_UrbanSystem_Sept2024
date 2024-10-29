using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;

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
        public async Task<IActionResult> Index()
        {
            var suggestions = await _context.Suggestions
                .ToListAsync();

            return View(suggestions);
        }
    }
}

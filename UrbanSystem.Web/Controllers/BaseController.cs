using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using UrbanSystem.Data;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Web.Controllers
{
	public class BaseController : Controller
	{
		private readonly ApplicationDbContext? _context;

        public BaseController(ApplicationDbContext? context = null)
        {
            _context = context;
        }

        protected bool IsGuidIdValid(string? id, ref Guid locationGuid)
		{
			if (string.IsNullOrWhiteSpace(id))
			{
				return false;
			}

			return Guid.TryParse(id.ToLower(), out locationGuid);
        }

		protected async Task<SuggestionFormViewModel> LoadLocations()
		{
            var cities = await _context.Locations
                .Select(l => new CityOption
                {
                    Value = l.Id.ToString(),
                    Text = l.CityName
                })
                .ToListAsync();

            var viewModel = new SuggestionFormViewModel
            {
                Cities = cities
            };

            return viewModel;
        }
	}
}

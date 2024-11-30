using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Services.Data
{
    public class MySuggestionService : IMySuggestionService
    {
        private readonly IRepository<ApplicationUserSuggestion, object> _userSuggestionRepository;

        public MySuggestionService(IRepository<ApplicationUserSuggestion, object> userSuggestionRepository)
        {
            _userSuggestionRepository = userSuggestionRepository;
        }

        public async Task<IEnumerable<MySuggestionsViewModel>> GetAllSuggestionsForLoggedInUser(string userId)
        {
            var suggestions = await _userSuggestionRepository
                .GetAllAttached()
                .Include(us => us.Suggestion)
                .ThenInclude(s => s.SuggestionsLocations)
                    .ThenInclude(sl => sl.Location)
                .Where(us => us.ApplicationUserId.ToString().ToLower() == userId.ToLower())
                .OrderBy(us => us.Suggestion.UploadedOn)
                .ToListAsync();

            var viewModel = suggestions.Select(us => new MySuggestionsViewModel
            {
                Id = us.Suggestion.Id.ToString(),
                Title = us.Suggestion.Title,
                Category = us.Suggestion.Category,
                UploadedOn = us.Suggestion.UploadedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                AttachmentUrl = us.Suggestion.AttachmentUrl,
                Upvotes = us.Suggestion.Upvotes.ToString(),
                Downvotes = us.Suggestion.Downvotes.ToString(),
                LocationNames = us.Suggestion.SuggestionsLocations.Select(sl => new CityOption
                {
                    Value = sl.Location.Id.ToString(),
                    Text = sl.Location.CityName
                }).ToList()
            });

            return viewModel;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Services.Mapping;
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
                .Where(us => us.ApplicationUserId.ToString().ToLower() == userId.ToLower())
                .OrderBy(us => us.Suggestion.UploadedOn)
                .ToListAsync();

            var viewModel = AutoMapperConfig.MapperInstance.Map<IEnumerable<MySuggestionsViewModel>>(suggestions);

            return viewModel;
        }
    }
}

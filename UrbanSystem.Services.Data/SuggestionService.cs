using Microsoft.AspNetCore.Identity;
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
    public class SuggestionService : ISuggestionService
    {
        private readonly IRepository<Suggestion, Guid> _suggestionRepository;
        private readonly IRepository<Location, Guid> _locationRepository;
        private readonly IRepository<ApplicationUserSuggestion, object> _userSuggestionRepository;
        private readonly IRepository<SuggestionLocation, object> _suggestionLocationRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuggestionService(IRepository<Suggestion, Guid> suggestionRepository, IRepository<Location, Guid> locationRepository, IRepository<ApplicationUserSuggestion, object> userSuggestionRepository, IRepository<SuggestionLocation, object> suggestionLocationRepository, UserManager<ApplicationUser> userManager)
        {
            _suggestionRepository = suggestionRepository;
            _locationRepository = locationRepository;
            _userSuggestionRepository = userSuggestionRepository;
            _suggestionLocationRepository = suggestionLocationRepository;
            _userManager = userManager;
        }

        public async Task<bool> AddSuggestionAsync(SuggestionFormViewModel suggestionModel, string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return false;
            }

            if (!Guid.TryParse(userId, out Guid parsedUserId))
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var location = await _locationRepository.GetByIdAsync(suggestionModel.CityId);

            if (location == null)
            {
                return false;
            }

            Suggestion suggestion = new Suggestion();
            AutoMapperConfig.MapperInstance.Map(suggestionModel, suggestion);

            await _suggestionRepository.AddAsync(suggestion);

            var applicationUserSuggestion = new ApplicationUserSuggestion
            {
                ApplicationUserId = user.Id,
                SuggestionId = suggestion.Id
            };

            await _userSuggestionRepository.AddAsync(applicationUserSuggestion);

            var suggestionLocation = new SuggestionLocation
            {
                SuggestionId = suggestion.Id,
                LocationId = location.Id
            };

            await _suggestionLocationRepository.AddAsync(suggestionLocation);

            return true;
        }

        public async Task<IEnumerable<SuggestionIndexViewModel>> GetAllSuggestionsAsync()
        {
            return await _suggestionRepository
                .GetAllAttached()
                .To<SuggestionIndexViewModel>()
                .ToListAsync();
        }

        public async Task<SuggestionIndexViewModel?> GetSuggestionDetailsAsync(Guid id)
        {
            var suggestion = await _suggestionRepository
                .GetByIdAsync(id);

            SuggestionIndexViewModel model = new SuggestionIndexViewModel();

            if (suggestion != null)
            {
                AutoMapperConfig.MapperInstance.Map(suggestion, model);
            }

            return model;
        }
    }
}

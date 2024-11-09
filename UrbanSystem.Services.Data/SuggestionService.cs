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

        public SuggestionService(IRepository<Suggestion, Guid> suggestionRepository)
        {
            _suggestionRepository = suggestionRepository;
        }

        public async Task<IEnumerable<SuggestionIndexViewModel>> GetAllSuggestionsAsync()
        {
            return await _suggestionRepository
                .GetAllAttached()
                .To<SuggestionIndexViewModel>()
                .ToListAsync();
        }
    }
}

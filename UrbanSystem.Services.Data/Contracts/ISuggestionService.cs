using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface ISuggestionService
    {
        Task<IEnumerable<SuggestionIndexViewModel>> GetAllSuggestionsAsync();

        Task<bool> AddSuggestionAsync(SuggestionFormViewModel suggestionModel, string userId);


    }
}

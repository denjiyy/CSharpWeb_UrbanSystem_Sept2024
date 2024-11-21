using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface ISuggestionService
    {
        Task<IEnumerable<SuggestionIndexViewModel>> GetAllSuggestionsAsync();
        Task<bool> AddSuggestionAsync(SuggestionFormViewModel suggestionModel, string userId);
        Task<SuggestionIndexViewModel?> GetSuggestionDetailsAsync(Guid id, string userId);
        Task<bool> AddCommentAsync(Guid suggestionId, string content, string userId);
        Task<bool> VoteCommentAsync(Guid commentId, string userId, bool isUpvote);
        Task<CommentViewModel?> GetCommentAsync(Guid commentId);
        Task<bool> UpdateSuggestionAsync(Guid id, SuggestionFormViewModel model, string userId);
        Task<SuggestionFormViewModel?> GetSuggestionForEditAsync(Guid id, ApplicationUser user);
        Task<bool> DeleteSuggestionAsync(Guid id, string userId);
        Task<ConfirmDeleteViewModel?> GetSuggestionForDeleteConfirmationAsync(Guid id, string userId);
    }
}
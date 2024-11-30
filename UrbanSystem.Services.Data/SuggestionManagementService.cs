using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Services.Data
{
    public class SuggestionManagementService : ISuggestionManagementService
    {
        private readonly IRepository<Suggestion, Guid> _suggestionRepository;
        private readonly IRepository<Location, Guid> _locationRepository;

        public SuggestionManagementService(
            IRepository<Suggestion, Guid> suggestionRepository,
            IRepository<Location, Guid> locationRepository)
        {
            _suggestionRepository = suggestionRepository;
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<SuggestionIndexViewModel>> GetAllSuggestionsAsync()
        {
            var suggestions = await _suggestionRepository.GetAllAsync();
            return suggestions.Select(s => new SuggestionIndexViewModel
            {
                Id = s.Id.ToString(),
                Title = s.Title,
                Category = s.Category,
                OrganizerName = s.UsersSuggestions.FirstOrDefault()?.User.UserName ?? "Unknown",
                AttachmentUrl = s.AttachmentUrl,
                Description = s.Description,
                UploadedOn = s.UploadedOn.ToString("yyyy-MM-dd"),
                Status = s.Status,
                Upvotes = s.Upvotes,
                Downvotes = s.Downvotes,
                Priority = s.Priority,
                LocationNames = s.SuggestionsLocations.Select(sl => sl.Location.CityName)
            });
        }

        public async Task<SuggestionIndexViewModel?> GetSuggestionByIdAsync(Guid id)
        {
            var suggestion = await _suggestionRepository.GetByIdAsync(id);
            if (suggestion == null)
            {
                return null;
            }

            return new SuggestionIndexViewModel
            {
                Id = suggestion.Id.ToString(),
                Title = suggestion.Title,
                Category = suggestion.Category,
                OrganizerName = suggestion.UsersSuggestions.FirstOrDefault()?.User.UserName ?? "Unknown",
                AttachmentUrl = suggestion.AttachmentUrl,
                Description = suggestion.Description,
                UploadedOn = suggestion.UploadedOn.ToString("yyyy-MM-dd"),
                Status = suggestion.Status,
                Upvotes = suggestion.Upvotes,
                Downvotes = suggestion.Downvotes,
                Priority = suggestion.Priority,
                LocationNames = suggestion.SuggestionsLocations.Select(sl => sl.Location.CityName),
                Comments = suggestion.Comments.Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content,
                    AddedOn = c.AddedOn,
                    UserName = c.User.UserName
                }).ToList()
            };
        }

        public async Task<bool> CreateSuggestionAsync(SuggestionFormViewModel model)
        {
            var suggestion = new Suggestion
            {
                Title = model.Title,
                Category = model.Category,
                AttachmentUrl = model.AttachmentUrl,
                Description = model.Description,
                Status = model.Status,
                Priority = model.Priority
            };

            var location = (await _locationRepository.GetAllAsync(l => l.CityName == model.CityName && l.StreetName == model.StreetName)).FirstOrDefault();
            if (location == null)
            {
                location = new Location
                {
                    CityName = model.CityName,
                    StreetName = model.StreetName
                };
                await _locationRepository.AddAsync(location);
            }

            suggestion.SuggestionsLocations.Add(new SuggestionLocation { Location = location });

            await _suggestionRepository.AddAsync(suggestion);
            return true;
        }

        public async Task<SuggestionFormViewModel?> GetSuggestionForEditAsync(Guid id)
        {
            var suggestion = await _suggestionRepository.GetByIdAsync(id);
            if (suggestion == null)
            {
                return null;
            }

            var location = suggestion.SuggestionsLocations.FirstOrDefault()?.Location;

            return new SuggestionFormViewModel
            {
                Id = suggestion.Id,
                Title = suggestion.Title,
                Category = suggestion.Category,
                AttachmentUrl = suggestion.AttachmentUrl,
                Description = suggestion.Description,
                Status = suggestion.Status,
                Priority = suggestion.Priority,
                CityName = location?.CityName ?? "",
                StreetName = location?.StreetName ?? ""
            };
        }

        public async Task<bool> UpdateSuggestionAsync(Guid id, SuggestionFormViewModel model)
        {
            var suggestion = await _suggestionRepository.GetByIdAsync(id);
            if (suggestion == null)
            {
                return false;
            }

            suggestion.Title = model.Title;
            suggestion.Category = model.Category;
            suggestion.AttachmentUrl = model.AttachmentUrl;
            suggestion.Description = model.Description;
            suggestion.Status = model.Status;
            suggestion.Priority = model.Priority;

            var locationList = await _locationRepository.GetAllAsync(l => l.CityName == model.CityName && l.StreetName == model.StreetName);
            var location = locationList.FirstOrDefault();
            if (location == null)
            {
                location = new Location
                {
                    CityName = model.CityName,
                    StreetName = model.StreetName
                };
                await _locationRepository.AddAsync(location);
            }

            suggestion.SuggestionsLocations.Clear();
            suggestion.SuggestionsLocations.Add(new SuggestionLocation { Location = location });

            return await _suggestionRepository.UpdateAsync(suggestion);
        }

        public async Task<bool> DeleteSuggestionAsync(Guid id)
        {
            return await _suggestionRepository.DeleteAsync(id);
        }

        public async Task<bool> UpdateSuggestionStatusAsync(Guid id, string status)
        {
            var suggestion = await _suggestionRepository.GetByIdAsync(id);
            if (suggestion == null)
            {
                return false;
            }

            suggestion.Status = status;
            return await _suggestionRepository.UpdateAsync(suggestion);
        }

        public async Task<bool> UpdateSuggestionPriorityAsync(Guid id, string priority)
        {
            var suggestion = await _suggestionRepository.GetByIdAsync(id);
            if (suggestion == null)
            {
                return false;
            }

            suggestion.Priority = priority;
            return await _suggestionRepository.UpdateAsync(suggestion);
        }
    }
}
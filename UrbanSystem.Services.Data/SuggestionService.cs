﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.Suggestions;

namespace UrbanSystem.Services.Data
{
    public class SuggestionService : ISuggestionService
    {
        private readonly IRepository<Suggestion, Guid> _suggestionRepository;
        private readonly IRepository<Location, Guid> _locationRepository;
        private readonly IRepository<ApplicationUserSuggestion, object> _userSuggestionRepository;
        private readonly IRepository<SuggestionLocation, object> _suggestionLocationRepository;
        private readonly IRepository<Comment, Guid> _commentRepository;
        private readonly IRepository<CommentVote, object> _commentVoteRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public SuggestionService(
            IRepository<Suggestion, Guid> suggestionRepository,
            IRepository<Location, Guid> locationRepository,
            IRepository<ApplicationUserSuggestion, object> userSuggestionRepository,
            IRepository<SuggestionLocation, object> suggestionLocationRepository,
            IRepository<Comment, Guid> commentRepository,
            IRepository<CommentVote, object> commentVoteRepository,
            UserManager<ApplicationUser> userManager)
        {
            _suggestionRepository = suggestionRepository;
            _locationRepository = locationRepository;
            _userSuggestionRepository = userSuggestionRepository;
            _suggestionLocationRepository = suggestionLocationRepository;
            _commentRepository = commentRepository;
            _commentVoteRepository = commentVoteRepository;
            _userManager = userManager;
        }

        public async Task<IEnumerable<SuggestionIndexViewModel>> GetAllSuggestionsAsync()
        {
            var suggestions = await _suggestionRepository.GetAllAsync();

            return suggestions.Select(suggestion => new SuggestionIndexViewModel
            {
                Id = suggestion.Id.ToString(),
                Title = suggestion.Title,
                Category = suggestion.Category,
                Description = suggestion.Description,
                AttachmentUrl = suggestion.AttachmentUrl,
                UploadedOn = suggestion.UploadedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = suggestion.Status,
                Priority = suggestion.Priority,
                Upvotes = suggestion.Upvotes,
                Downvotes = suggestion.Downvotes
            });
        }

        public async Task<SuggestionFormViewModel> GetSuggestionFormViewModelAsync()
        {
            var cities = await _locationRepository.GetAllAsync();
            return new SuggestionFormViewModel
            {
                Cities = cities.Select(c => new CityOption { Value = c.Id.ToString(), Text = c.CityName }).ToList()
            };
        }

        public async Task<(bool IsSuccessful, SuggestionFormViewModel ViewModel, string ErrorMessage)> AddSuggestionAsync(SuggestionFormViewModel suggestionModel, string userId)
        {
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return (false, suggestionModel, "Invalid user ID.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, suggestionModel, "User not found.");
            }

            var location = await _locationRepository.GetByIdAsync(suggestionModel.CityId);
            if (location == null)
            {
                return (false, suggestionModel, "Invalid city selected.");
            }

            var suggestion = new Suggestion
            {
                Title = suggestionModel.Title,
                Category = suggestionModel.Category,
                Description = suggestionModel.Description,
                AttachmentUrl = suggestionModel.AttachmentUrl,
                Status = suggestionModel.Status,
                Priority = suggestionModel.Priority,
                UploadedOn = DateTime.UtcNow
            };

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

            return (true, null, null);
        }

        public async Task<(bool IsSuccessful, SuggestionIndexViewModel Suggestion, string ErrorMessage)> GetSuggestionDetailsAsync(string id, string userId)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                return (false, null, "Invalid suggestion ID.");
            }

            var suggestion = await _suggestionRepository
                 .GetAllAttached()
                 .Include(s => s.Comments)
                 .ThenInclude(c => c.User)
                 .Include(s => s.UsersSuggestions)
                 .ThenInclude(us => us.User)
                 .FirstOrDefaultAsync(s => s.Id == suggestionId);

            if (suggestion == null)
            {
                return (false, null, "Suggestion not found.");
            }

            bool isOwner = false;

            if (!string.IsNullOrEmpty(userId))
            {
                if (Guid.TryParse(userId, out var parsedUserId))
                {
                    isOwner = suggestion.UsersSuggestions.Any(us => us.ApplicationUserId == parsedUserId);
                }
            }

            var viewModel = new SuggestionIndexViewModel
            {
                Id = suggestion.Id.ToString(),
                Title = suggestion.Title,
                Category = suggestion.Category,
                Description = suggestion.Description,
                AttachmentUrl = suggestion.AttachmentUrl,
                UploadedOn = suggestion.UploadedOn.ToString("yyyy-MM-dd HH:mm:ss"),
                Status = suggestion.Status,
                Priority = suggestion.Priority,
                Upvotes = suggestion.Upvotes,
                Downvotes = suggestion.Downvotes,
                LocationNames = suggestion.SuggestionsLocations.Select(sl => sl.Location.CityName).ToList(),
                Comments = suggestion.Comments.Select(c => new CommentViewModel
                {
                    Id = c.Id,
                    Content = c.Content ?? string.Empty,
                    AddedOn = c.AddedOn,
                    UserName = c.User?.UserName ?? "Unknown User",
                    Upvotes = c.Upvotes,
                    Downvotes = c.Downvotes
                }).ToList(),
                OrganizerName = string.Join(", ", suggestion.UsersSuggestions.Select(x => x.User.UserName))
            };

            return (true, viewModel, null);
        }

        public async Task<(bool IsSuccessful, string ErrorMessage)> AddCommentAsync(string suggestionId, string content, string userId)
        {
            if (!Guid.TryParse(suggestionId, out Guid parsedSuggestionId))
            {
                return (false, "Invalid suggestion ID.");
            }

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return (false, "Invalid user ID.");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return (false, "User not found.");
            }

            var comment = new Comment
            {
                Content = content,
                UserId = parsedUserId,
                SuggestionId = parsedSuggestionId
            };

            await _commentRepository.AddAsync(comment);
            return (true, null);
        }

        public async Task<(bool IsSuccessful, CommentViewModel Comment, string ErrorMessage)> VoteCommentAsync(string commentId, string userId, bool isUpvote)
        {
            if (!Guid.TryParse(commentId, out Guid parsedCommentId))
            {
                return (false, null, "Invalid comment ID.");
            }

            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return (false, null, "Invalid user ID.");
            }

            var comment = await _commentRepository.GetByIdAsync(parsedCommentId);
            if (comment == null)
            {
                return (false, null, "Comment not found.");
            }

            var existingVote = await _commentVoteRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(cv => cv.CommentId == parsedCommentId && cv.UserId == parsedUserId);

            if (existingVote != null)
            {
                if (existingVote.IsUpvote == isUpvote)
                {
                    await _commentVoteRepository.DeleteAsync(existingVote.Id);
                    if (isUpvote)
                    {
                        comment.Upvotes = Math.Max(comment.Upvotes - 1, 0);
                    }
                    else
                    {
                        comment.Downvotes = Math.Max(comment.Downvotes - 1, 0);
                    }
                }
                else
                {
                    await _commentVoteRepository.DeleteAsync(existingVote.Id);
                    if (existingVote.IsUpvote)
                    {
                        comment.Upvotes = Math.Max(comment.Upvotes - 1, 0);
                        comment.Downvotes++;
                    }
                    else
                    {
                        comment.Downvotes = Math.Max(comment.Downvotes - 1, 0);
                        comment.Upvotes++;
                    }
                }
            }
            else
            {
                if (isUpvote)
                {
                    comment.Upvotes++;
                }
                else
                {
                    comment.Downvotes++;
                }
            }

            var newVote = new CommentVote
            {
                CommentId = parsedCommentId,
                UserId = parsedUserId,
                IsUpvote = isUpvote
            };

            await _commentVoteRepository.AddAsync(newVote);
            await _commentRepository.UpdateAsync(comment);

            return (true, new CommentViewModel
            {
                Id = comment.Id,
                Content = comment.Content ?? string.Empty,
                AddedOn = comment.AddedOn,
                UserName = comment.User.UserName,
                Upvotes = comment.Upvotes,
                Downvotes = comment.Downvotes
            }, null);
        }

        public async Task<CommentViewModel?> GetCommentAsync(Guid commentId)
        {
            if (commentId == Guid.Empty)
            {
                return null;
            }

            var comment = await _commentRepository.GetAllAttached()
                .Include(c => c.User)
                .FirstOrDefaultAsync(c => c.Id == commentId);

            if (comment == null)
            {
                return null;
            }

            return new CommentViewModel
            {
                Id = comment.Id,
                Content = comment.Content ?? string.Empty,
                AddedOn = comment.AddedOn,
                UserName = comment.User?.UserName ?? "Unknown User",
                Upvotes = comment.Upvotes,
                Downvotes = comment.Downvotes
            };
        }

        public async Task<(bool IsSuccessful, SuggestionFormViewModel ViewModel, string ErrorMessage)> GetSuggestionForEditAsync(string id, ApplicationUser user)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                return (false, null, "Invalid suggestion ID.");
            }

            var suggestion = await _suggestionRepository
            .GetAllAttached()
            .Include(s => s.SuggestionsLocations)
            .ThenInclude(sl => sl.Location)
            .Include(s => s.UsersSuggestions)
            .FirstOrDefaultAsync(s => s.Id == suggestionId);

            if (suggestion == null)
            {
                return (false, null, "Suggestion not found.");
            }

            // Check if the current user is the original poster
            var isOriginalPoster = suggestion.UsersSuggestions.Any(us => us.ApplicationUserId == user.Id);
            if (!isOriginalPoster)
            {
                return (false, null, "You are not authorized to edit this suggestion.");
            }

            var cities = await _locationRepository.GetAllAsync();

            var viewModel = new SuggestionFormViewModel
            {
                Id = suggestion.Id,
                Title = suggestion.Title,
                Category = suggestion.Category,
                Description = suggestion.Description,
                AttachmentUrl = suggestion.AttachmentUrl,
                Status = suggestion.Status,
                Priority = suggestion.Priority,
                CityId = suggestion.SuggestionsLocations.FirstOrDefault()?.LocationId ?? Guid.Empty,
                Cities = cities.Select(c => new CityOption { Value = c.Id.ToString(), Text = c.CityName }).ToList(),
                UserId = user.Id.ToString()
            };

            return (true, viewModel, null);
        }

        public async Task<(bool IsSuccessful, string ErrorMessage)> UpdateSuggestionAsync(string id, SuggestionFormViewModel model, string userId)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                return (false, "Invalid suggestion ID.");
            }

            var suggestion = await _suggestionRepository
                .GetAllAttached()
                .Include(s => s.SuggestionsLocations)
                .Include(s => s.UsersSuggestions)
                .FirstOrDefaultAsync(s => s.Id == suggestionId);

            if (suggestion == null)
            {
                return (false, "Suggestion not found.");
            }

            var userSuggestion = suggestion.UsersSuggestions.FirstOrDefault();
            if (userSuggestion == null || userSuggestion.ApplicationUserId.ToString() != userId)
            {
                return (false, "You are not authorized to edit this suggestion.");
            }

            suggestion.Title = model.Title;
            suggestion.Category = model.Category;
            suggestion.Description = model.Description;
            suggestion.AttachmentUrl = model.AttachmentUrl;
            suggestion.Status = model.Status;
            suggestion.Priority = model.Priority;

            var existingLocation = suggestion.SuggestionsLocations.FirstOrDefault();
            if (existingLocation != null && existingLocation.LocationId != model.CityId)
            {
                await _suggestionLocationRepository.DeleteAsync(sl => sl.SuggestionId == suggestion.Id && sl.LocationId == existingLocation.LocationId);

                var newLocation = new SuggestionLocation
                {
                    SuggestionId = suggestion.Id,
                    LocationId = model.CityId
                };
                await _suggestionLocationRepository.AddAsync(newLocation);
            }

            await _suggestionRepository.UpdateAsync(suggestion);

            return (true, null);
        }

        public async Task<(bool IsSuccessful, ConfirmDeleteViewModel ViewModel, string ErrorMessage)> GetSuggestionForDeleteConfirmationAsync(string id, string userId)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                return (false, null, "Invalid suggestion ID.");
            }

            var suggestion = await _suggestionRepository
                .GetAllAttached()
                .Include(s => s.UsersSuggestions)
                .FirstOrDefaultAsync(s => s.Id == suggestionId);

            if (suggestion == null)
            {
                return (false, null, "Suggestion not found.");
            }

            var userSuggestion = suggestion.UsersSuggestions.FirstOrDefault();
            if (userSuggestion == null || userSuggestion.ApplicationUserId.ToString() != userId)
            {
                return (false, null, "You are not authorized to delete this suggestion.");
            }

            var viewModel = new ConfirmDeleteViewModel
            {
                Id = suggestion.Id,
                Title = suggestion.Title,
                Category = suggestion.Category,
                Description = suggestion.Description
            };

            return (true, viewModel, null);
        }

        public async Task<(bool IsSuccessful, string ErrorMessage)> DeleteSuggestionAsync(string id, string userId)
        {
            if (!Guid.TryParse(id, out Guid suggestionId))
            {
                return (false, "Invalid suggestion ID.");
            }

            var suggestion = await _suggestionRepository
                .GetAllAttached()
                .Include(s => s.UsersSuggestions)
                .FirstOrDefaultAsync(s => s.Id == suggestionId);

            if (suggestion == null)
            {
                return (false, "Suggestion not found.");
            }

            var userSuggestion = suggestion.UsersSuggestions.FirstOrDefault();
            if (userSuggestion == null || userSuggestion.ApplicationUserId.ToString() != userId)
            {
                return (false, "You are not authorized to delete this suggestion.");
            }

            await _suggestionLocationRepository.DeleteAsync(sl => sl.SuggestionId == suggestion.Id);
            await _userSuggestionRepository.DeleteAsync(us => us.SuggestionId == suggestion.Id);

            var comments = await _commentRepository.GetAllAttached()
                .Where(c => c.SuggestionId == suggestion.Id)
                .ToListAsync();

            foreach (var comment in comments)
            {
                await _commentVoteRepository.DeleteAsync(cv => cv.CommentId == comment.Id);
                await _commentRepository.DeleteAsync(comment.Id);
            }

            await _suggestionRepository.DeleteAsync(suggestion.Id);

            return (true, null);
        }

        public Task<bool> DeleteSuggestionAsync(Guid id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ConfirmDeleteViewModel?> GetSuggestionForDeleteConfirmationAsync(Guid id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}
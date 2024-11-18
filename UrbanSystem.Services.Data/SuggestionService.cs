﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels;
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

        public async Task<bool> AddSuggestionAsync(SuggestionFormViewModel suggestionModel, string userId)
        {
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
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

            return true;
        }

        public async Task<IEnumerable<SuggestionIndexViewModel>> GetAllSuggestionsAsync()
        {
            // Fetch all suggestions from the database
            var suggestions = await _suggestionRepository.GetAllAsync();

            // Manually map each Suggestion to a SuggestionIndexViewModel
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

        public async Task<SuggestionIndexViewModel?> GetSuggestionDetailsAsync(Guid id)
        {
            var suggestion = await _suggestionRepository
                .GetAllAttached()
                .Include(s => s.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (suggestion == null)
            {
                return null;
            }

            // Manually map the Suggestion to a SuggestionIndexViewModel
            return new SuggestionIndexViewModel
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
                }).ToList()
            };
        }

        public async Task<bool> AddCommentAsync(Guid suggestionId, string content, string userId)
        {
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var comment = new Comment
            {
                Content = content,
                UserId = parsedUserId,
                SuggestionId = suggestionId
            };

            await _commentRepository.AddAsync(comment);
            return true;
        }

        public async Task<bool> VoteCommentAsync(Guid commentId, string userId, bool isUpvote)
        {
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return false;
            }

            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return false;
            }

            var existingVote = await _commentVoteRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(cv => cv.CommentId == commentId && cv.UserId == parsedUserId);

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
                CommentId = commentId,
                UserId = parsedUserId,
                IsUpvote = isUpvote
            };

            await _commentVoteRepository.AddAsync(newVote);
            await _commentRepository.UpdateAsync(comment);

            return true;
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
    }
}

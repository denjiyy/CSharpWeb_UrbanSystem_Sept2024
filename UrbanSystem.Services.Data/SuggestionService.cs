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

        public SuggestionService(IRepository<Suggestion, Guid> suggestionRepository, IRepository<Location, Guid> locationRepository, IRepository<ApplicationUserSuggestion, object> userSuggestionRepository,
            IRepository<SuggestionLocation, object> suggestionLocationRepository, IRepository<Comment, Guid> commentRepository,
            IRepository<CommentVote, object> commentVoteRepository, UserManager<ApplicationUser> userManager)
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
                .GetAllAttached()
                .Include(s => s.Comments)
                    .ThenInclude(c => c.User)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (suggestion == null)
            {
                return null;
            }

            var model = AutoMapperConfig.MapperInstance.Map<SuggestionIndexViewModel>(suggestion);

            return model;
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
            // Validate userId
            if (string.IsNullOrEmpty(userId) || !Guid.TryParse(userId, out Guid parsedUserId))
            {
                return false;
            }

            // Check if the comment exists
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                return false;
            }

            // Fetch the existing vote if any
            var existingVote = await _commentVoteRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(cv => cv.CommentId == commentId && cv.UserId == parsedUserId);

            if (existingVote != null)
            {
                // Handle existing vote
                if (existingVote.IsUpvote == isUpvote)
                {
                    // Same vote type: remove vote
                    await _commentVoteRepository.DeleteAsync(existingVote.Id);
                    if (isUpvote)
                    {
                        comment.Upvotes = Math.Max(comment.Upvotes - 1, 0);  // Ensure no negative votes
                    }
                    else
                    {
                        comment.Downvotes = Math.Max(comment.Downvotes - 1, 0);  // Ensure no negative votes
                    }
                }
                else
                {
                    // Different vote type: switch vote
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
                // New vote
                if (isUpvote)
                {
                    comment.Upvotes++;
                }
                else
                {
                    comment.Downvotes++;
                }
            }

            // Add the new vote
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
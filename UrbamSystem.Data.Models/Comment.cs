using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Models
{
    public class Comment
    {
        public Comment()
        {
            Id = Guid.NewGuid();
            CommentVotes = new HashSet<CommentVote>();
        }

        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public Guid SuggestionId { get; set; }
        public Suggestion Suggestion { get; set; } = null!;

        public int Upvotes { get; set; } = 0;
        public int Downvotes { get; set; } = 0;

        public ICollection<CommentVote> CommentVotes { get; set; }
    }
}
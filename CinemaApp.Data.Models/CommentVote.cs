using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Models
{
    public class CommentVote
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid CommentId { get; set; }  // Foreign key
        public Comment Comment { get; set; } = null!;  // Navigation property

        public Guid UserId { get; set; }  // Foreign key
        public ApplicationUser User { get; set; } = null!;  // Navigation property

        public bool IsUpvote { get; set; }  // Indicates an upvote or downvote
    }
}

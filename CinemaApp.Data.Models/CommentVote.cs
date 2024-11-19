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

        public Guid CommentId { get; set; }
        public Comment Comment { get; set; } = null!;

        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        public bool IsUpvote { get; set; }
    }
}

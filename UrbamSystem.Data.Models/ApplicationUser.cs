using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace UrbanSystem.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        public virtual ICollection<ApplicationUserSuggestion> UsersSuggestions { get; set; } = new HashSet<ApplicationUserSuggestion>();
        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
        public virtual ICollection<CommentVote> CommentsVotes { get; set; } = new HashSet<CommentVote>();
        public virtual ICollection<Meeting> Meetings { get; set; } = new HashSet<Meeting>();
        public virtual ICollection<Meeting> OrganizedMeetings { get; set; } = new HashSet<Meeting>();
        public virtual ICollection<Suggestion> Suggestions { get; set; } = new HashSet<Suggestion>();
    }
}
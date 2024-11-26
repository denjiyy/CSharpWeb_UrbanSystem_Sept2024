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
        }

        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;
        public Guid SuggestionId { get; set; }
        public Suggestion Suggestion { get; set; } = null!;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Data.Models
{
    public class ApplicationUserSuggestion
    {
        public Guid ApplicationUserId { get; set; }
        public virtual ApplicationUser User { get; set; } = null!;

        public Guid SuggestionId { get; set; }
        public virtual Suggestion Suggestion { get; set; } = null!;
    }
}
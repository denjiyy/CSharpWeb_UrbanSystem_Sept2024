using System;
using System.Collections.Generic;
using System.Linq;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Web.ViewModels.Suggestions
{
    public class SuggestionIndexViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string OrganizerName { get; set; } = null!;

        public string? AttachmentUrl { get; set; }

        public string Description { get; set; } = null!;

        public string UploadedOn { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public string Priority { get; set; } = "Medium";

        public IEnumerable<string> LocationNames { get; set; } = new HashSet<string>();
        public ICollection<CommentViewModel> Comments { get; set; } = new HashSet<CommentViewModel>();
    }
}

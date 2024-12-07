﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrbanSystem.Data.Models
{
    public class Suggestion
    {
        public Suggestion()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string? AttachmentUrl { get; set; }

        public string Description { get; set; } = null!;

        public DateTime UploadedOn { get; set; } = DateTime.UtcNow.Date;

        public string Status { get; set; } = null!;

        public int Upvotes { get; set; } = 0;

        public int Downvotes { get; set; } = 0;

        public string Priority { get; set; } = "Medium";

        public virtual ICollection<SuggestionLocation> SuggestionsLocations { get; set; } = new HashSet<SuggestionLocation>();

        public virtual ICollection<ApplicationUserSuggestion> UsersSuggestions { get; set; } = new HashSet<ApplicationUserSuggestion>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
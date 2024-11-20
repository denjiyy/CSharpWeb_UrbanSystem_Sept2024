﻿using UrbanSystem.Data.Models;

namespace UrbanSystem.Web.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = null!;
        public DateTime AddedOn { get; set; }
        public string UserName { get; set; } = null!;
        public int Upvotes { get; set; }
        public int Downvotes { get; set; }

    }
}
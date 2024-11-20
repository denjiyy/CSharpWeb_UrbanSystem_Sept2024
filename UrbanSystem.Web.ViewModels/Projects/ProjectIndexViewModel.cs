﻿using System;

namespace UrbanSystem.Web.ViewModels.Projects
{
    public class ProjectIndexViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public decimal Amount { get; set; }
        public decimal DesiredSum { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public bool IsCompleted { get; set; }
        public string? LocationName { get; set; }
    }
}
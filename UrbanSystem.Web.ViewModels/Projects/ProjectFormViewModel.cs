using System;
using System.ComponentModel.DataAnnotations;

namespace UrbanSystem.Web.ViewModels.Projects
{
    public class ProjectFormViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; } = null!;

        [StringLength(2048, ErrorMessage = "Image URL must be less than 2048 characters.")]
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
        public string Description { get; set; } = null!;

        public bool IsCompleted { get; set; }

        [Required]
        public Guid LocationId { get; set; }
    }
}

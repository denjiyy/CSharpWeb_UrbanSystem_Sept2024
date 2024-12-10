using System.ComponentModel.DataAnnotations;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Web.ViewModels.Projects
{
    public class ProjectFormViewModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name must be less than 100 characters.")]
        public string Name { get; set; } = null!;

        [Required]
        [Range(0.01, 10000000, ErrorMessage = "Desired sum must be between 0.01 and 10,000,000.")]
        public decimal DesiredSum { get; set; }

        [StringLength(2048, ErrorMessage = "Image URL must be less than 2048 characters.")]
        public string? ImageUrl { get; set; }

        [Required]
        [StringLength(500, ErrorMessage = "Description must be less than 500 characters.")]
        public string Description { get; set; } = null!;

        [Required]
        public DateTime FundingDeadline { get; set; }

        public bool IsCompleted { get; set; }

        [Required]
        public Guid LocationId { get; set; }

        [Required]
        public IEnumerable<CityOption> Cities { get; set; } = new HashSet<CityOption>();
    }
}

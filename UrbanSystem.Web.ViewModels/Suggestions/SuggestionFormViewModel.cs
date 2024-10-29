using System.ComponentModel.DataAnnotations;
using static UrbanSystem.Common.EntityValidationConstants.Suggestion;

namespace UrbanSystem.Web.ViewModels.Suggestions
{
    public class SuggestionFormViewModel
    {
        [Required]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(CategoryMinLength)]
        [MaxLength(CategoryMaxLength)]
        public string Category { get; set; } = null!;

        public string? AttachmentUrl { get; set; }

        [Required]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required]
        [MaxLength(CityNameMaxLength)]
        public string CityName { get; set; } = null!;

        [Required]
        public string Priority { get; set; } = "Medium";

        [Required]
        public string Status { get; set; } = "Open";

        [Required]
        [MinLength(StreetMinLength)]
        [MaxLength(StreetMaxLength)]
        public string StreetName { get; set; } = null!;
    }
}

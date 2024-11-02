using System.ComponentModel.DataAnnotations;
using static UrbanSystem.Common.EntityValidationConstants.Suggestion;
using static UrbanSystem.Common.EntityValidationMessages.Suggestion;

namespace UrbanSystem.Web.ViewModels.Suggestions
{
    public class SuggestionFormViewModel
    {
        [Required(ErrorMessage = TitleRequiredMessage)]
        [MinLength(TitleMinLength)]
        [MaxLength(TitleMaxLength)]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = CategoryRequiredMessage)]
        [MinLength(CategoryMinLength)]
        [MaxLength(CategoryMaxLength)]
        public string Category { get; set; } = null!;

        public string? AttachmentUrl { get; set; }

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [MinLength(DescriptionMinLength)]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = CityNameRequiredMessage)]
        [MaxLength(CityNameMaxLength)]
        public string CityName { get; set; } = null!;

        [Required(ErrorMessage = PriorityRequiredMessage)]
        public string Priority { get; set; } = "Medium";

        [Required(ErrorMessage = StatusRequiredMessage)]
        public string Status { get; set; } = "Open";

        [Required(ErrorMessage = StreetNameRequiredMessage)]
        [MinLength(StreetMinLength)]
        [MaxLength(StreetMaxLength)]
        public string StreetName { get; set; } = null!;
    }
}

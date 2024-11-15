using System;
using System.ComponentModel.DataAnnotations;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class MeetingFormViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot be longer than 200 characters")]
        public string Title { get; set; } = null!;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1000, ErrorMessage = "Description cannot be longer than 1000 characters")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Scheduled date is required")]
        [Display(Name = "Scheduled Date")]
        public DateTime ScheduledDate { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Display(Name = "Duration (hours)")]
        [Range(0.5, 8, ErrorMessage = "Duration must be between 0.5 and 8 hours")]
        public double Duration { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(200, ErrorMessage = "Location cannot be longer than 200 characters")]
        public string Location { get; set; } = null!;

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; } = "Scheduled";
    }
}
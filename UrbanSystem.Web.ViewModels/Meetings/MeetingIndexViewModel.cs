using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class MeetingIndexViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Display(Name = "Scheduled Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ScheduledDate { get; set; }

        [Display(Name = "Duration")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan Duration { get; set; }

        [Display(Name = "Location")]
        public Guid LocationId { get; set; }

        [Display(Name = "City Name")]
        public string? CityName { get; set; }

        [Display(Name = "Attendees Count")]
        public int AttendeesCount { get; set; }

        [Display(Name = "Attendees")]
        public IEnumerable<string> Attendees { get; set; } = new HashSet<string>();

        [Display(Name = "Organizer")]
        public string OrganizerName { get; set; } = null!;

        public Guid OrganizerId { get; set; }

        public bool IsCurrentUserOrganizer { get; set; }
    }
}
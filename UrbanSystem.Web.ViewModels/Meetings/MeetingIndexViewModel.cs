using System.ComponentModel.DataAnnotations;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class MeetingIndexViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ScheduledDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan Duration { get; set; }

        public Guid LocationId { get; set; }

        public string? CityName { get; set; }
        
        public int AttendeesCount { get; set; }
        
        public IEnumerable<string> Attendees { get; set; } = new HashSet<string>();

        public string OrganizerName { get; set; } = null!;

        public Guid OrganizerId { get; set; }

        public bool IsCurrentUserOrganizer { get; set; }
    }
}
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class MeetingEditViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTime ScheduledDate { get; set; }

        public TimeSpan Duration { get; set; }

        public string? LocationId { get; set; }

        public IEnumerable<CityOption> Cities { get; set; } = new HashSet<CityOption>();
    }
}

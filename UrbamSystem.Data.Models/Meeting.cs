namespace UrbanSystem.Data.Models
{
    public class Meeting
    {
        public Meeting()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }
        public TimeSpan Duration { get; set; }
        public Guid LocationId { get; set; }
        public Location Location { get; set; } = null!;
        public Guid OrganizerId { get; set; }
        public ApplicationUser Organizer { get; set; } = null!;

        public virtual ICollection<ApplicationUser> Attendees { get; set; } = new HashSet<ApplicationUser>();
    }
}
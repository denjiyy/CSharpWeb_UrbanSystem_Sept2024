using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Data.Configuration
{
    public class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(m => m.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(m => m.ScheduledDate)
                .IsRequired();

            builder.Property(m => m.Duration)
                .IsRequired();

            builder.HasOne(m => m.Location)
                .WithMany()
                .HasForeignKey(m => m.LocationId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(m => m.Attendees)
                .WithMany(u => u.Meetings);

            builder.HasOne(m => m.Organizer)
                .WithMany(u => u.OrganizedMeetings)
                .HasForeignKey(m => m.OrganizerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
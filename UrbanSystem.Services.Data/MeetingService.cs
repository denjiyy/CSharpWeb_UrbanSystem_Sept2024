using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Meetings;

namespace UrbanSystem.Services.Data
{
    public class MeetingService : IMeetingService
    {
        private readonly ApplicationDbContext _context;

        public MeetingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MeetingIndexViewModel>> GetAllMeetingsAsync()
        {
            return await _context.Meetings
                .Select(m => new MeetingIndexViewModel
                {
                    Id = m.Id,
                    Title = m.Title,
                    Description = m.Description,
                    ScheduledDate = m.ScheduledDate,
                    Duration = m.Duration,
                    Location = m.Location
                })
                .ToListAsync();
        }

        public async Task<MeetingIndexViewModel> GetMeetingByIdAsync(Guid id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                return null!;
            }

            return new MeetingIndexViewModel
            {
                Id = meeting.Id,
                Title = meeting.Title,
                Description = meeting.Description,
                ScheduledDate = meeting.ScheduledDate,
                Duration = meeting.Duration,
                Location = meeting.Location
            };
        }

        public async Task<Guid> CreateMeetingAsync(MeetingFormViewModel meetingForm)
        {
            var meeting = new Meeting
            {
                Title = meetingForm.Title,
                Description = meetingForm.Description,
                ScheduledDate = meetingForm.ScheduledDate,
                Duration = TimeSpan.FromHours(meetingForm.Duration),
                Location = meetingForm.Location
            };

            _context.Meetings.Add(meeting);
            await _context.SaveChangesAsync();

            return meeting.Id;
        }

        public async Task UpdateMeetingAsync(Guid id, MeetingFormViewModel meetingForm)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                throw new ArgumentException("Meeting not found", nameof(id));
            }

            meeting.Title = meetingForm.Title;
            meeting.Description = meetingForm.Description;
            meeting.ScheduledDate = meetingForm.ScheduledDate;
            meeting.Duration = TimeSpan.FromHours(meetingForm.Duration);
            meeting.Location = meetingForm.Location;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteMeetingAsync(Guid id)
        {
            var meeting = await _context.Meetings.FindAsync(id);
            if (meeting == null)
            {
                throw new ArgumentException("Meeting not found", nameof(id));
            }

            _context.Meetings.Remove(meeting);
            await _context.SaveChangesAsync();
        }
    }
}
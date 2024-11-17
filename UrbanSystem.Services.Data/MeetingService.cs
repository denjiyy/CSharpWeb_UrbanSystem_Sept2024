using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Meetings;

namespace UrbanSystem.Services.Data
{
    public class MeetingService : IMeetingService
    {
        private readonly IRepository<Meeting, Guid> _meetingRepository;
        private readonly IRepository<Location, Guid> _locationRepository; // Added location repository
        private readonly IRepository<ApplicationUser, Guid> _userRepository;

        public MeetingService(
            IRepository<Meeting, Guid> meetingRepository,
            IRepository<Location, Guid> locationRepository,
            IRepository<ApplicationUser, Guid> userRepository)
        {
            _meetingRepository = meetingRepository;
            _locationRepository = locationRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<MeetingIndexViewModel>> GetAllMeetingsAsync()
        {
            var meetings = await _meetingRepository.GetAllAsync();
            return meetings.Select(m => new MeetingIndexViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ScheduledDate = m.ScheduledDate,
                Duration = m.Duration,
                CityName = m.Location?.CityName ?? "Unknown" // Include location name
            });
        }

        public async Task<MeetingIndexViewModel> GetMeetingByIdAsync(Guid id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
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
                CityName = meeting.Location?.CityName ?? "Unknown" // Include location name
            };
        }

        public async Task<Guid> CreateMeetingAsync(MeetingFormViewModel meetingForm)
        {
            var location = await _locationRepository.GetByIdAsync(meetingForm.LocationId ?? Guid.Empty);
            if (location == null)
            {
                throw new ArgumentException("Invalid location ID.");
            }

            var meeting = new Meeting
            {
                Title = meetingForm.Title,
                Description = meetingForm.Description,
                ScheduledDate = meetingForm.ScheduledDate,
                Duration = TimeSpan.FromHours(meetingForm.Duration),
                LocationId = location.Id // Set location
            };

            await _meetingRepository.AddAsync(meeting);
            return meeting.Id;
        }

        public async Task UpdateMeetingAsync(Guid id, MeetingFormViewModel meetingForm)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            if (meeting == null)
            {
                throw new ArgumentException("Meeting not found", nameof(id));
            }

            var location = await _locationRepository.GetByIdAsync(meetingForm.LocationId ?? Guid.Empty);
            if (location == null)
            {
                throw new ArgumentException("Invalid location ID.");
            }

            meeting.Title = meetingForm.Title;
            meeting.Description = meetingForm.Description;
            meeting.ScheduledDate = meetingForm.ScheduledDate;
            meeting.Duration = TimeSpan.FromHours(meetingForm.Duration);
            meeting.LocationId = location.Id; // Update location

            await _meetingRepository.UpdateAsync(meeting);
        }

        public async Task DeleteMeetingAsync(Guid id)
        {
            var success = await _meetingRepository.DeleteAsync(id);
            if (!success)
            {
                throw new ArgumentException("Meeting not found", nameof(id));
            }
        }

        public async Task AttendMeetingAsync(string username, Guid meetingId)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
            {
                throw new ArgumentException("Meeting not found.");
            }

            var user = _userRepository.GetAllAttached().FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            if (meeting.Attendees.Any(a => a.Id == user.Id))
            {
                throw new ArgumentException("User already attending.");
            }

            meeting.Attendees.Add(user);
            await _meetingRepository.UpdateAsync(meeting);
        }

        public async Task CancelAttendanceAsync(string username, Guid meetingId)
        {
            var meeting = await _meetingRepository.GetByIdAsync(meetingId);
            if (meeting == null)
            {
                throw new ArgumentException("Meeting not found.");
            }

            var user = _userRepository.GetAllAttached().FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                throw new ArgumentException("User not found.");
            }

            var attendee = meeting.Attendees.FirstOrDefault(a => a.Id == user.Id);
            if (attendee == null)
            {
                throw new ArgumentException("Attendance record not found.");
            }

            meeting.Attendees.Remove(attendee);
            await _meetingRepository.UpdateAsync(meeting);
        }

        public async Task<IEnumerable<AttendedMeetingViewModel>> GetUserAttendedMeetingsAsync(string username)
        {
            var user = await _userRepository.GetAllAttached()
                .Include(u => u.Meetings)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return Enumerable.Empty<AttendedMeetingViewModel>();
            }

            return user.Meetings.Select(m => new AttendedMeetingViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ScheduledDate = m.ScheduledDate,
                Duration = m.Duration,
                Location = m.Location?.CityName ?? "Unknown", // Include location name
                CanCancelAttendance = m.ScheduledDate > DateTime.Now.AddHours(24)
            });
        }
    }
}

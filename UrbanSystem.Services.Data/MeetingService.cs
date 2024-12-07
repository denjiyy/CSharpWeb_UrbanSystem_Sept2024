﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Locations;
using UrbanSystem.Web.ViewModels.Meetings;

namespace UrbanSystem.Services.Data
{
    public class MeetingService : IMeetingService
    {
        private readonly IRepository<Meeting, Guid> _meetingRepository;
        private readonly IRepository<Location, Guid> _locationRepository;
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
            var meetings = await _meetingRepository.GetAllAttached()
                .Include(m => m.Location)
                .Include(m => m.Attendees)
                .ToListAsync();

            return meetings.Select(m => new MeetingIndexViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ScheduledDate = m.ScheduledDate,
                Duration = m.Duration,
                CityName = m.Location?.CityName ?? "Unknown",
                AttendeesCount = m.Attendees.Count
            });
        }

        public async Task<MeetingIndexViewModel> GetMeetingByIdAsync(Guid id)
        {
            var meeting = await _meetingRepository.GetAllAttached()
                .Include(m => m.Location)
                .Include(m => m.Attendees)
                .Include(m => m.Organizer)
                .FirstOrDefaultAsync(m => m.Id == id);

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
                CityName = meeting.Location?.CityName ?? "Unknown",
                Attendees = meeting.Attendees.Select(a => a.UserName).ToList(),
                OrganizerName = meeting.Organizer.UserName,
                OrganizerId = meeting.OrganizerId
            };
        }

        public async Task<MeetingFormViewModel> GetMeetingFormViewModelAsync(MeetingFormViewModel existingModel = null)
        {
            var locations = await _locationRepository.GetAllAsync();
            var cities = locations.Select(l => new CityOption
            {
                Value = l.Id.ToString(),
                Text = l.CityName
            }).AsEnumerable();

            return new MeetingFormViewModel
            {
                Title = existingModel?.Title ?? string.Empty,
                Description = existingModel?.Description ?? string.Empty,
                ScheduledDate = existingModel?.ScheduledDate ?? DateTime.Now,
                Duration = existingModel?.Duration ?? 1,
                LocationId = existingModel?.LocationId,
                Cities = cities
            };
        }

        public async Task<MeetingFormViewModel> GetMeetingForEditAsync(Guid id)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            if (meeting == null)
            {
                return null!;
            }

            var viewModel = await GetMeetingFormViewModelAsync();
            viewModel.Title = meeting.Title;
            viewModel.Description = meeting.Description;
            viewModel.ScheduledDate = meeting.ScheduledDate;
            viewModel.Duration = meeting.Duration.TotalHours;
            viewModel.LocationId = meeting.LocationId;

            return viewModel;
        }

        public async Task<Guid> CreateMeetingAsync(MeetingFormViewModel meetingForm, string organizerUsername)
        {
            var location = await _locationRepository.GetByIdAsync(meetingForm.LocationId ?? Guid.Empty);
            if (location == null)
            {
                throw new ArgumentException("Invalid location ID.");
            }

            var organizer = await _userRepository.GetAllAttached().FirstOrDefaultAsync(u => u.UserName == organizerUsername);
            if (organizer == null)
            {
                throw new ArgumentException("Organizer not found.");
            }

            var meeting = new Meeting
            {
                Title = meetingForm.Title,
                Description = meetingForm.Description,
                ScheduledDate = meetingForm.ScheduledDate,
                Duration = TimeSpan.FromHours(meetingForm.Duration),
                LocationId = location.Id,
                Location = location,
                OrganizerId = organizer.Id,
                Organizer = organizer
            };

            await _meetingRepository.AddAsync(meeting);
            return meeting.Id;
        }

        public async Task UpdateMeetingAsync(Guid id, MeetingFormViewModel meetingForm)
        {
            var meeting = await _meetingRepository.GetAllAttached()
                .Include(m => m.Location)
                .FirstOrDefaultAsync(m => m.Id == id);

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
            meeting.LocationId = location.Id;
            meeting.Location = location;

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
            var meeting = await _meetingRepository.GetAllAttached()
                .Include(m => m.Attendees)
                .FirstOrDefaultAsync(m => m.Id == meetingId);

            if (meeting == null)
            {
                throw new InvalidOperationException("Meeting not found.");
            }

            var user = await _userRepository.GetAllAttached().FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            if (meeting.Attendees.Any(a => a.Id == user.Id))
            {
                throw new InvalidOperationException("You are already attending this meeting.");
            }

            meeting.Attendees.Add(user);
            await _meetingRepository.UpdateAsync(meeting);
        }

        public async Task CancelAttendanceAsync(string username, Guid meetingId)
        {
            var meeting = await _meetingRepository.GetAllAttached()
                .Include(m => m.Attendees)
                .FirstOrDefaultAsync(m => m.Id == meetingId);

            if (meeting == null)
            {
                throw new InvalidOperationException("Meeting not found.");
            }

            var user = await _userRepository.GetAllAttached().FirstOrDefaultAsync(u => u.UserName == username);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var attendee = meeting.Attendees.FirstOrDefault(a => a.Id == user.Id);
            if (attendee == null)
            {
                throw new InvalidOperationException("You are not attending this meeting.");
            }

            meeting.Attendees.Remove(attendee);
            await _meetingRepository.UpdateAsync(meeting);
        }

        public async Task<UserAttendedMeetingsViewModel> GetUserAttendedMeetingsAsync(string username)
        {
            var user = await _userRepository.GetAllAttached()
                .Include(u => u.Meetings)
                .ThenInclude(m => m.Location)
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
            {
                return new UserAttendedMeetingsViewModel { AttendedMeetings = new List<AttendedMeetingViewModel>() };
            }

            var attendedMeetings = user.Meetings.Select(m => new AttendedMeetingViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ScheduledDate = m.ScheduledDate,
                Duration = m.Duration,
                Location = m.Location?.CityName ?? "Unknown",
                CanCancelAttendance = m.ScheduledDate > DateTime.Now.AddHours(24)
            }).ToList();

            return new UserAttendedMeetingsViewModel { AttendedMeetings = attendedMeetings };
        }
    }
}
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data.Models;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Meetings;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Services.Data
{
    public class MeetingManagementService : IMeetingManagementService
    {
        private readonly IRepository<Meeting, Guid> _meetingRepository;
        private readonly IRepository<Location, Guid> _locationRepository;
        private readonly IRepository<ApplicationUser, string> _userRepository;

        public MeetingManagementService(
            IRepository<Meeting, Guid> meetingRepository,
            IRepository<Location, Guid> locationRepository,
            IRepository<ApplicationUser, string> userRepository)
        {
            _meetingRepository = meetingRepository;
            _locationRepository = locationRepository;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<MeetingIndexViewModel>> GetAllMeetingsAsync()
        {
            var meetings = await _meetingRepository.GetAllAttached()
                .Include(m => m.Location)
                .Include(m => m.Organizer)
                .Include(m => m.Attendees)
                .ToListAsync();

            return meetings.Select(m => new MeetingIndexViewModel
            {
                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                ScheduledDate = m.ScheduledDate,
                Duration = m.Duration,
                LocationId = m.LocationId,
                CityName = m.Location.CityName,
                AttendeesCount = m.Attendees.Count,
                Attendees = m.Attendees.Select(a => a.UserName)!,
                OrganizerName = m.Organizer.UserName!,
                OrganizerId = m.OrganizerId,
                IsCurrentUserOrganizer = false
            });
        }

        public async Task<MeetingIndexViewModel?> GetMeetingByIdAsync(Guid id)
        {
            var meeting = await _meetingRepository.GetAllAttached()
                .Include(m => m.Location)
                .Include(m => m.Organizer)
                .Include(m => m.Attendees)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meeting == null)
            {
                return null;
            }

            return new MeetingIndexViewModel
            {
                Id = meeting.Id,
                Title = meeting.Title,
                Description = meeting.Description,
                ScheduledDate = meeting.ScheduledDate,
                Duration = meeting.Duration,
                LocationId = meeting.LocationId,
                CityName = meeting.Location.CityName,
                AttendeesCount = meeting.Attendees.Count,
                Attendees = meeting.Attendees.Select(a => a.UserName)!,
                OrganizerName = meeting.Organizer.UserName!,
                OrganizerId = meeting.OrganizerId,
                IsCurrentUserOrganizer = false
            };
        }

        public async Task<MeetingEditViewModel?> GetMeetingForEditAsync(Guid id)
        {
            var meeting = await _meetingRepository.GetAllAttached()
                .Include(m => m.Location)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (meeting == null)
            {
                return null;
            }

            double durationInHours = meeting.Duration.TotalHours;

            return new MeetingEditViewModel
            {
                Id = meeting.Id,
                Title = meeting.Title,
                Description = meeting.Description,
                ScheduledDate = meeting.ScheduledDate,
                Duration = durationInHours,
                LocationId = meeting.LocationId.ToString(),
                Cities = await GetAllCitiesAsync()
            };
        }

        public async Task<bool> UpdateMeetingAsync(Guid id, MeetingEditViewModel model)
        {
            var meeting = await _meetingRepository.GetByIdAsync(id);
            if (meeting == null)
            {
                return false;
            }

            meeting.Title = model.Title;
            meeting.Description = model.Description;
            meeting.ScheduledDate = model.ScheduledDate;

            meeting.Duration = TimeSpan.FromHours(model.Duration);

            meeting.LocationId = Guid.Parse(model.LocationId!);

            return await _meetingRepository.UpdateAsync(meeting);
        }

        public async Task<bool> DeleteMeetingAsync(Guid id)
        {
            return await _meetingRepository.DeleteAsync(id);
        }

        public async Task<bool> CreateMeetingAsync(MeetingFormViewModel model)
        {
            var meeting = new Meeting
            {
                Title = model.Title,
                Description = model.Description,
                ScheduledDate = model.ScheduledDate,
                Duration = TimeSpan.FromHours(model.Duration),
                LocationId = model.LocationId.Value
            };

            await _meetingRepository.AddAsync(meeting);
            return true;
        }

        public async Task<IEnumerable<CityOption>> GetAllCitiesAsync()
        {
            var cities = await _locationRepository.GetAllAsync();
            return cities.Select(c => new CityOption
            {
                Value = c.Id.ToString(),
                Text = c.CityName
            });
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Meetings;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface IMeetingService
    {
        Task<IEnumerable<MeetingIndexViewModel>> GetAllMeetingsAsync();
        Task<MeetingIndexViewModel> GetMeetingByIdAsync(Guid id);
        Task<Guid> CreateMeetingAsync(MeetingFormViewModel meetingForm);
        Task UpdateMeetingAsync(Guid id, MeetingFormViewModel meetingForm);
        Task DeleteMeetingAsync(Guid id);
        Task AttendMeetingAsync(string username, Guid meetingId);
        Task CancelAttendanceAsync(string username, Guid meetingId);
        Task<IEnumerable<AttendedMeetingViewModel>> GetUserAttendedMeetingsAsync(string username);
    }
}
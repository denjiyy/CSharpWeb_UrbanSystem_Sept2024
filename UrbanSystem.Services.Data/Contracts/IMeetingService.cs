using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSystem.Web.ViewModels.Meetings;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface IMeetingService
    {
        Task<IEnumerable<MeetingIndexViewModel>> GetAllMeetingsAsync();
        Task<MeetingIndexViewModel> GetMeetingByIdAsync(Guid id);
        Task<MeetingFormViewModel> GetMeetingFormViewModelAsync(MeetingFormViewModel existingModel = null);
        Task<MeetingFormViewModel> GetMeetingForEditAsync(Guid id);
        Task<Guid> CreateMeetingAsync(MeetingFormViewModel meetingForm, string organizerName);
        Task UpdateMeetingAsync(Guid id, MeetingFormViewModel meetingForm);
        Task DeleteMeetingAsync(Guid id);
        Task AttendMeetingAsync(string username, Guid meetingId);
        Task CancelAttendanceAsync(string username, Guid meetingId);
        Task<UserAttendedMeetingsViewModel> GetUserAttendedMeetingsAsync(string username);
    }
}
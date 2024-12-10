using UrbanSystem.Web.ViewModels.Meetings;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface IMeetingManagementService
    {
        Task<IEnumerable<MeetingIndexViewModel>> GetAllMeetingsAsync();
        Task<MeetingIndexViewModel?> GetMeetingByIdAsync(Guid id);
        Task<MeetingEditViewModel?> GetMeetingForEditAsync(Guid id);
        Task<bool> UpdateMeetingAsync(Guid id, MeetingEditViewModel model);
        Task<bool> DeleteMeetingAsync(Guid id);
        Task<bool> CreateMeetingAsync(MeetingFormViewModel model);
        Task<IEnumerable<CityOption>> GetAllCitiesAsync();
    }
}
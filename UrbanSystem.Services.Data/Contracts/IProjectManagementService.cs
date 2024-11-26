using UrbanSystem.Web.ViewModels.Projects;

namespace UrbanSystem.Services.Data.Contracts
{
    public interface IProjectManagementService
    {
        Task<IEnumerable<ProjectIndexViewModel>> GetAllProjectsAsync();
        Task<ProjectIndexViewModel?> GetProjectByIdAsync(Guid id);
        Task<ProjectFormViewModel?> GetProjectForEditAsync(Guid id);
        Task<bool> UpdateProjectAsync(Guid id, ProjectFormViewModel model);
        Task<bool> DeleteProjectAsync(Guid id);
        Task<bool> ToggleProjectCompletionAsync(Guid id);
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UrbanSystem.Data.Repository.Contracts;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Interfaces;
using UrbanSystem.Web.ViewModels.Projects;

namespace UrbanSystem.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IRepository<Project, Guid> _projectRepository;
        private readonly IRepository<Location, Guid> _locationRepository;

        public ProjectService(IRepository<Project, Guid> projectRepository, IRepository<Location, Guid> locationRepository)
        {
            _projectRepository = projectRepository;
            _locationRepository = locationRepository;
        }

        public async Task<IEnumerable<ProjectIndexViewModel>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository
                .GetAllAttached()
                .Include(p => p.Location)
                .Select(p => new ProjectIndexViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    ImageUrl = p.ImageUrl,
                    Description = p.Description,
                    CreatedOn = p.CreatedOn,
                    IsCompleted = p.IsCompleted,
                    LocationName = p.Location.CityName
                })
                .ToListAsync();

            return projects;
        }

        public async Task<ProjectIndexViewModel?> GetProjectByIdAsync(Guid id)
        {
            var project = await _projectRepository
                .GetAllAttached()
                .Include(p => p.Location)
                .Include(p => p.ProjectFundings)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return null;
            }

            return new ProjectIndexViewModel
            {
                Id = project.Id,
                Name = project.Name,
                ImageUrl = project.ImageUrl,
                Description = project.Description,
                CreatedOn = project.CreatedOn,
                IsCompleted = project.IsCompleted,
                LocationName = project.Location.CityName,
                Fundings = project.ProjectFundings.Select(f => new ProjectFundingViewModel
                {
                    Amount = f.Funding.Amount,
                    FundedOn = f.Funding.CreatedOn
                }).ToList()
            };
        }

        public async Task AddProjectAsync(ProjectFormViewModel project)
        {
            var newProject = new Project
            {
                Name = project.Name,
                ImageUrl = project.ImageUrl,
                Description = project.Description,
                IsCompleted = project.IsCompleted,
                LocationId = project.LocationId,
                CreatedOn = DateTime.UtcNow
            };

            await _projectRepository.AddAsync(newProject);
        }

        public async Task DeleteProjectAsync(Guid id)
        {
            await _projectRepository.DeleteAsync(id);
        }
    }
}

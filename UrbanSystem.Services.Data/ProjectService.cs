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

        public ProjectService(IRepository<Project, Guid> projectRepository)
        {
            _projectRepository = projectRepository;
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
                    DesiredSum = p.FundsNeeded,
                    Amount = p.FundsRaised,
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
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return null;
            }

            return new ProjectIndexViewModel
            {
                Id = project.Id,
                Name = project.Name,
                DesiredSum = project.FundsNeeded,
                Amount = project.FundsRaised,
                ImageUrl = project.ImageUrl,
                Description = project.Description,
                CreatedOn = project.CreatedOn,
                IsCompleted = project.IsCompleted,
                LocationName = project.Location.CityName
            };
        }

        public async Task AddProjectAsync(ProjectFormViewModel project)
        {
            if (project == null)
            {
                throw new ArgumentNullException(nameof(project));
            }

            if (project.LocationId == Guid.Empty)
            {
                throw new ArgumentException("Invalid location ID.");
            }

            var newProject = new Project
            {
                Name = project.Name,
                ImageUrl = project.ImageUrl,
                Description = project.Description,
                FundsNeeded = project.DesiredSum,
                IsCompleted = project.IsCompleted,
                LocationId = project.LocationId,
                CreatedOn = DateTime.UtcNow,
                FundingDeadline = project.FundingDeadline
            };

            await _projectRepository.AddAsync(newProject);
        }

        public async Task<bool> DeleteProjectAsync(Guid id)
        {
            var project = await _projectRepository
                .GetAllAttached()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (project == null)
            {
                return false;
            }

            await _projectRepository.DeleteAsync(id);
            return true;
        }
    }
}

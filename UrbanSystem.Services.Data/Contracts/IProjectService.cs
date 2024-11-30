﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Projects;

namespace UrbanSystem.Services.Interfaces
{
    public interface IProjectService
    {
        Task<IEnumerable<ProjectIndexViewModel>> GetAllProjectsAsync();
        Task<ProjectIndexViewModel?> GetProjectByIdAsync(Guid id);
        Task AddProjectAsync(ProjectFormViewModel project);
        Task<bool> DeleteProjectAsync(Guid id);
    }
}

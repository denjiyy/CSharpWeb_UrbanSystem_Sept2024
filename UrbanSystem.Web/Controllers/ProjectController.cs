using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Services.Interfaces;
using UrbanSystem.Web.ViewModels.Projects;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private readonly IBaseService _baseService;
        private readonly ILogger<ProjectController> _logger;

        public ProjectController(
            IBaseService baseService,
            IProjectService projectService,
            ILogger<ProjectController> logger) : base(baseService)
        {
            _projectService = projectService ?? throw new ArgumentNullException(nameof(projectService));
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                var projects = await _projectService.GetAllProjectsAsync();
                return View(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all projects");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid project ID.");
            }

            try
            {
                var project = await _projectService.GetProjectByIdAsync(id);

                if (project == null)
                {
                    _logger.LogWarning("Project not found: {ProjectId}", id);
                    return NotFound("The requested project could not be found.");
                }

                return View(project);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching project details for ID: {ProjectId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var cities = await CityList();

                var viewModel = new ProjectFormViewModel
                {
                    Cities = cities
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while preparing the Add Project form");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] ProjectFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    model.Cities = await CityList();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while fetching city list for invalid Add Project form");
                }
                return View(model);
            }

            try
            {
                await _projectService.AddProjectAsync(model);
                _logger.LogInformation("New project added successfully");
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while adding a new project");
                ModelState.AddModelError(string.Empty, "An error occurred while adding the project. Please try again.");
                model.Cities = await CityList();
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid project ID.");
            }

            try
            {
                var isDeleted = await _projectService.DeleteProjectAsync(id);

                if (!isDeleted)
                {
                    _logger.LogWarning("Attempt to delete non-existent project: {ProjectId}", id);
                    return NotFound("The project you're trying to delete could not be found.");
                }

                _logger.LogInformation("Project deleted successfully: {ProjectId}", id);
                return RedirectToAction(nameof(All));
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Invalid operation while deleting project: {ProjectId}", id);
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Details), new { id });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting project: {ProjectId}", id);
                TempData["ErrorMessage"] = "An unexpected error occurred while deleting the project.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}


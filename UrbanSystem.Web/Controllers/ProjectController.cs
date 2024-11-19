using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Services.Interfaces;
using UrbanSystem.Web.ViewModels.Projects;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;
        private new readonly IBaseService _baseService;

        public ProjectController(IBaseService baseService, IProjectService projectService) : base(baseService)
        {
            _projectService = projectService;
            _baseService = baseService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var projects = await _projectService.GetAllProjectsAsync();
            return View(projects);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var cities = await CityList();

            var viewModel = new ProjectFormViewModel
            {
                Cities = cities
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(ProjectFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);

                model.Cities = await CityList();
                return View(model);
            }

            await _projectService.AddProjectAsync(model);
            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var isDeleted = await _projectService.DeleteProjectAsync(id);

                if (!isDeleted)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(All));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction(nameof(Details), new { id });
            }
        }
    }
}

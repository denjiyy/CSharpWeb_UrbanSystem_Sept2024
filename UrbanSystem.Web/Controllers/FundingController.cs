using Microsoft.AspNetCore.Mvc;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Services.Interfaces;
using UrbanSystem.Web.ViewModels.Funding;

namespace UrbanSystem.Web.Controllers
{
    public class FundingController : Controller
    {
        private readonly IFundingService _fundingService;
        private readonly IProjectService _projectService;

        public FundingController(IFundingService fundingService, IProjectService projectService)
        {
            _fundingService = fundingService;
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> Add(Guid projectId)
        {
            var project = await _projectService.GetProjectByIdAsync(projectId);
            if (project == null)
            {
                return NotFound();
            }

            var model = new FundingFormViewModel
            {
                ProjectId = projectId
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(FundingFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.IsConfirmed)
            {
                ModelState.AddModelError("IsConfirmed", "You must confirm the funding amount.");
                return View(model);
            }

            var success = await _fundingService.AddFundingAsync(model.ProjectId, model.Amount);
            if (success)
            {
                TempData["SuccessMessage"] = "Funding added successfully!";
                return RedirectToAction("Details", "Project", new { id = model.ProjectId });
            }
            else
            {
                ModelState.AddModelError("", "Failed to add funding. Please try again.");
                return View(model);
            }
        }
    }
}
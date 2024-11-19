using Microsoft.AspNetCore.Mvc;
using UrbanSystem.Services.Interfaces;
using UrbanSystem.Web.ViewModels.Funding;
using System;
using System.Threading.Tasks;
using UrbanSystem.Services.Data.Contracts;

namespace UrbanSystem.Web.Controllers
{
    public class FundingController : Controller
    {
        private readonly IFundingService _fundingService;

        public FundingController(IFundingService fundingService)
        {
            _fundingService = fundingService;
        }

        [HttpGet]
        public IActionResult Add(Guid projectId)
        {
            var viewModel = new FundingFormViewModel
            {
                ProjectId = projectId
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(FundingFormViewModel model, Guid projectId)
        {
            model.ProjectId = projectId;

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.IsConfirmed)
            {
                ModelState.AddModelError(string.Empty, "You must confirm the transaction to proceed.");
                return View(model);
            }
            
            var isSuccessful = await _fundingService.AddFundingAsync(model.ProjectId, model.Amount);

            if (isSuccessful)
            {
                TempData["SuccessMessage"] = "Funding has been successfully added!";
                return RedirectToAction("Details", "Project", new { id = model.ProjectId });
            }

            ModelState.AddModelError(string.Empty, "There was an issue processing your funding.");
            return View(model);
        }
    }
}

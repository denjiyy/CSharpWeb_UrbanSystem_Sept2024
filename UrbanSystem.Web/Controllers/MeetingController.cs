using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Meetings;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class MeetingController : BaseController
    {
        private readonly IMeetingService _meetingService;
        private readonly IBaseService _baseService;
        private readonly UserManager<ApplicationUser> _userManager;

        public MeetingController(IBaseService baseService, IMeetingService meetingService, UserManager<ApplicationUser> userManager) : base(baseService)
        {
            _meetingService = meetingService;
            _baseService = baseService;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var meetings = await _meetingService.GetAllMeetingsAsync();
            return View(meetings);
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var viewModel = await _meetingService.GetMeetingFormViewModelAsync();
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MeetingFormViewModel meetingForm)
        {
            if (!ModelState.IsValid)
            {
                meetingForm = await _meetingService.GetMeetingFormViewModelAsync(meetingForm);
                return View(meetingForm);
            }

            await _meetingService.CreateMeetingAsync(meetingForm);
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var meetingForm = await _meetingService.GetMeetingForEditAsync(id);
            if (meetingForm == null)
            {
                return NotFound();
            }

            return View(meetingForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MeetingFormViewModel meetingForm)
        {
            if (!ModelState.IsValid)
            {
                meetingForm = await _meetingService.GetMeetingFormViewModelAsync(meetingForm);
                return View(meetingForm);
            }

            try
            {
                await _meetingService.UpdateMeetingAsync(id, meetingForm);
                return RedirectToAction(nameof(All));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                await _meetingService.DeleteMeetingAsync(id);
                return RedirectToAction(nameof(All));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }
            return View(meeting);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Attend(Guid id)
        {
            if (User.Identity?.Name == null)
            {
                return Unauthorized();
            }

            try
            {
                await _meetingService.AttendMeetingAsync(User.Identity.Name, id);
                TempData["SuccessMessage"] = "You have successfully registered for the meeting!";
            }
            catch (InvalidOperationException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAttendance(Guid id)
        {
            if (User.Identity?.Name == null)
            {
                return Unauthorized();
            }

            try
            {
                await _meetingService.CancelAttendanceAsync(User.Identity.Name, id);
            }
            catch (InvalidOperationException ex)
            {
                return View(ex.Message);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> MyMeetings()
        {
            if (User.Identity?.Name == null)
            {
                return Unauthorized();
            }

            var viewModel = await _meetingService.GetUserAttendedMeetingsAsync(User.Identity.Name);
            return View(viewModel);
        }
    }
}
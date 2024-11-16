using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Meetings;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    public class MeetingController : BaseController
    {
        private readonly IMeetingService _meetingService;

        public MeetingController(IMeetingService meetingService)
        {
            _meetingService = meetingService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            var meetings = await _meetingService.GetAllMeetingsAsync();
            return View(meetings);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(MeetingFormViewModel meetingForm)
        {
            if (ModelState.IsValid)
            {
                await _meetingService.CreateMeetingAsync(meetingForm);
                return RedirectToAction(nameof(All));
            }
            return View(meetingForm);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var meeting = await _meetingService.GetMeetingByIdAsync(id);
            if (meeting == null)
            {
                return NotFound();
            }

            var meetingForm = new MeetingFormViewModel
            {
                Title = meeting.Title,
                Description = meeting.Description,
                ScheduledDate = meeting.ScheduledDate,
                Duration = meeting.Duration.TotalHours,
                Location = meeting.Location
            };

            return View(meetingForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, MeetingFormViewModel meetingForm)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _meetingService.UpdateMeetingAsync(id, meetingForm);
                }
                catch (ArgumentException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(All));
            }
            return View(meetingForm);
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
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(All));
        }

        [HttpGet]
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
            try
            {
                if (User.Identity?.Name == null)
                {
                    return Unauthorized();
                }

                await _meetingService.AttendMeetingAsync(User.Identity.Name, id);
                TempData["SuccessMessage"] = "You have successfully registered for the meeting!";
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = "Unable to attend the meeting. Please try again.";
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CancelAttendance(Guid id)
        {
            try
            {
                if (User.Identity?.Name == null)
                {
                    return Unauthorized();
                }

                await _meetingService.CancelAttendanceAsync(User.Identity.Name, id);
                TempData["SuccessMessage"] = "You have successfully canceled your attendance!";
            }
            catch (ArgumentException)
            {
                TempData["ErrorMessage"] = "Unable to cancel attendance. Please try again.";
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

            var attendedMeetings = await _meetingService.GetUserAttendedMeetingsAsync(User.Identity.Name);
            var viewModel = new UserAttendedMeetingsViewModel
            {
                AttendedMeetings = attendedMeetings
            };
            return View(viewModel);
        }
    }
}
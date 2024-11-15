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
    }
}
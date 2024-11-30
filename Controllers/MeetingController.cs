using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Data.Contracts;
using UrbanSystem.Web.ViewModels.Meetings;

namespace UrbanSystem.Web.Controllers
{
    [Authorize]
    [AutoValidateAntiforgeryToken]
    public class MeetingController : BaseController
    {
        private readonly IMeetingService _meetingService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<MeetingController> _logger;

        public MeetingController(
            IBaseService baseService,
            IMeetingService meetingService,
            UserManager<ApplicationUser> userManager,
            ILogger<MeetingController> logger) : base(baseService)
        {
            _meetingService = meetingService ?? throw new ArgumentNullException(nameof(meetingService));
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> All()
        {
            try
            {
                var meetings = await _meetingService.GetAllMeetingsAsync();
                var currentUser = await _userManager.GetUserAsync(User);
                foreach (var meeting in meetings)
                {
                    meeting.IsCurrentUserOrganizer = currentUser != null && meeting.OrganizerId == currentUser.Id;
                }
                return View(meetings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching all meetings");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            try
            {
                var viewModel = await _meetingService.GetMeetingFormViewModelAsync();
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while preparing the Add Meeting form");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] MeetingFormViewModel meetingForm)
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    meetingForm = await _meetingService.GetMeetingFormViewModelAsync(meetingForm);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while repopulating the Add Meeting form");
                }
                return View(meetingForm);
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            try
            {
                await _meetingService.CreateMeetingAsync(meetingForm, currentUser.UserName);
                _logger.LogInformation("New meeting created by user {UserId}", currentUser.Id);
                return RedirectToAction(nameof(All));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating a new meeting");
                ModelState.AddModelError(string.Empty, "An error occurred while creating the meeting. Please try again.");
                meetingForm = await _meetingService.GetMeetingFormViewModelAsync(meetingForm);
                return View(meetingForm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid meeting ID.");
            }

            try
            {
                var meeting = await _meetingService.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    return NotFound();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null || meeting.OrganizerId != currentUser.Id)
                {
                    _logger.LogWarning("Unauthorized edit attempt for meeting {MeetingId} by user {UserId}", id, currentUser?.Id);
                    return Forbid();
                }

                var meetingForm = await _meetingService.GetMeetingForEditAsync(id);
                return View(meetingForm);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while preparing to edit meeting {MeetingId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Guid id, [FromForm] MeetingFormViewModel meetingForm)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid meeting ID.");
            }

            if (!ModelState.IsValid)
            {
                try
                {
                    meetingForm = await _meetingService.GetMeetingFormViewModelAsync(meetingForm);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while repopulating the Edit Meeting form");
                }
                return View(meetingForm);
            }

            try
            {
                var meeting = await _meetingService.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    return NotFound();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null || meeting.OrganizerId != currentUser.Id)
                {
                    _logger.LogWarning("Unauthorized edit attempt for meeting {MeetingId} by user {UserId}", id, currentUser?.Id);
                    return Forbid();
                }

                await _meetingService.UpdateMeetingAsync(id, meetingForm);
                _logger.LogInformation("Meeting {MeetingId} updated by user {UserId}", id, currentUser.Id);
                return RedirectToAction(nameof(All));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating meeting {MeetingId}", id);
                ModelState.AddModelError(string.Empty, "An error occurred while updating the meeting. Please try again.");
                meetingForm = await _meetingService.GetMeetingFormViewModelAsync(meetingForm);
                return View(meetingForm);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid meeting ID.");
            }

            try
            {
                var meeting = await _meetingService.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    return NotFound();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null || meeting.OrganizerId != currentUser.Id)
                {
                    _logger.LogWarning("Unauthorized delete attempt for meeting {MeetingId} by user {UserId}", id, currentUser?.Id);
                    return Forbid();
                }

                return View(meeting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while preparing to delete meeting {MeetingId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid meeting ID.");
            }

            try
            {
                var meeting = await _meetingService.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    return NotFound();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null || meeting.OrganizerId != currentUser.Id)
                {
                    _logger.LogWarning("Unauthorized delete attempt for meeting {MeetingId} by user {UserId}", id, currentUser?.Id);
                    return Forbid();
                }

                await _meetingService.DeleteMeetingAsync(id);
                _logger.LogInformation("Meeting {MeetingId} deleted by user {UserId}", id, currentUser.Id);
                return RedirectToAction(nameof(All));
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting meeting {MeetingId}", id);
                TempData["ErrorMessage"] = "An error occurred while deleting the meeting. Please try again.";
                return RedirectToAction(nameof(Details), new { id });
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid meeting ID.");
            }

            try
            {
                var meeting = await _meetingService.GetMeetingByIdAsync(id);
                if (meeting == null)
                {
                    return NotFound();
                }

                var currentUser = await _userManager.GetUserAsync(User);
                meeting.IsCurrentUserOrganizer = currentUser != null && meeting.OrganizerId == currentUser.Id;

                return View(meeting);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching details for meeting {MeetingId}", id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Attend(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid meeting ID.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            try
            {
                await _meetingService.AttendMeetingAsync(currentUser.UserName, id);
                _logger.LogInformation("User {UserId} registered for meeting {MeetingId}", currentUser.Id, id);
                TempData["SuccessMessage"] = "You have successfully registered for the meeting!";
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error occurred while user {UserId} was attempting to attend meeting {MeetingId}", currentUser.Id, id);
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(All));
        }

        [HttpPost]
        public async Task<IActionResult> CancelAttendance(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("Invalid meeting ID.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            try
            {
                await _meetingService.CancelAttendanceAsync(currentUser.UserName, id);
                _logger.LogInformation("User {UserId} cancelled attendance for meeting {MeetingId}", currentUser.Id, id);
                TempData["SuccessMessage"] = "You have successfully cancelled your attendance.";
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, "Error occurred while user {UserId} was attempting to cancel attendance for meeting {MeetingId}", currentUser.Id, id);
                TempData["ErrorMessage"] = ex.Message;
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> MyMeetings()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Unauthorized();
            }

            try
            {
                var viewModel = await _meetingService.GetUserAttendedMeetingsAsync(currentUser.UserName);
                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching attended meetings for user {UserId}", currentUser.Id);
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
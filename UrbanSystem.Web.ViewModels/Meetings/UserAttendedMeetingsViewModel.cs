using System;
using System.Collections.Generic;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class UserAttendedMeetingsViewModel
    {
        public IEnumerable<AttendedMeetingViewModel> AttendedMeetings { get; set; } = new HashSet<AttendedMeetingViewModel>();
    }
}
using System;
using System.Collections.Generic;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class UserAttendedMeetingsViewModel
    {
        public IEnumerable<AttendedMeetingViewModel> AttendedMeetings { get; set; }
    }

    public class AttendedMeetingViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime ScheduledDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Location { get; set; }
        public bool CanCancelAttendance { get; set; }
    }
}
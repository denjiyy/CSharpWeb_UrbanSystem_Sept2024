using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class AttendedMeetingViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Location { get; set; } = null!;
        public bool CanCancelAttendance { get; set; }
    }
}

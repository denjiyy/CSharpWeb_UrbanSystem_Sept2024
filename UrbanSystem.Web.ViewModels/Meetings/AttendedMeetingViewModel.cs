using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Mapping;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class AttendedMeetingViewModel : IMapFrom<Meeting>, IHaveCustomMappings
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime ScheduledDate { get; set; }
        public TimeSpan Duration { get; set; }
        public string Location { get; set; } = null!;
        public bool CanCancelAttendance { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Meeting, AttendedMeetingViewModel>()
                .ForMember(dest => dest.CanCancelAttendance,
                           opt => opt.MapFrom(src => src.ScheduledDate > DateTime.Now.AddHours(24)));
        }
    }
}

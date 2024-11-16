using System;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Mapping;

namespace UrbanSystem.Web.ViewModels.Meetings
{
    public class MeetingIndexViewModel : IMapFrom<Meeting>, IHaveCustomMappings
    {
        public Guid Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; } = null!;

        [Display(Name = "Description")]
        public string Description { get; set; } = null!;

        [Display(Name = "Scheduled Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime ScheduledDate { get; set; }

        [Display(Name = "Duration")]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan Duration { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; } = null!;

        [Display(Name = "Attendees Count")]
        public int AttendeesCount { get; set; }

        public void CreateMappings(IProfileExpression configuration) 
        { 
            configuration.CreateMap<Meeting, MeetingIndexViewModel>()
                .ForMember(dest => dest.AttendeesCount, opt => opt
                .MapFrom(src => src.Attendees.Count)); 
        }
    }
}

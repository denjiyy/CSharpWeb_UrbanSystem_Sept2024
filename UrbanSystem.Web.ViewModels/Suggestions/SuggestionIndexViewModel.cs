using AutoMapper;
using System;
using System.Collections.Generic;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Mapping;

namespace UrbanSystem.Web.ViewModels.Suggestions
{
    public class SuggestionIndexViewModel : IMapFrom<Suggestion>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string Category { get; set; } = null!;

        public string? AttachmentUrl { get; set; }

        public string Description { get; set; } = null!;

        public string UploadedOn { get; set; } = null!;

        public string Status { get; set; } = null!;

        public int Upvotes { get; set; }

        public int Downvotes { get; set; }

        public string Priority { get; set; } = "Medium";

        public IEnumerable<string> LocationNames { get; set; } = new HashSet<string>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Suggestion, SuggestionIndexViewModel>()
                .ForMember(d => d.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(d => d.UploadedOn, opt => opt.MapFrom(src => src.UploadedOn.ToString("dd/MM/yyyy")))
                .ForMember(d => d.LocationNames, opt => opt.MapFrom(src => src.SuggestionsLocations.Select(sl => sl.Location.CityName)))
                .ForMember(d => d.Priority, opt => opt.MapFrom(src => src.Priority ?? "Medium"));
        }
    }
}

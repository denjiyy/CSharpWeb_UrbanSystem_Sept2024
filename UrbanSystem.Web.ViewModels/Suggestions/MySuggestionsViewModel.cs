using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Mapping;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Web.ViewModels.Suggestions
{
    public class MySuggestionsViewModel : IMapFrom<ApplicationUserSuggestion>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string UploadedOn { get; set; } = null!;
        public string? AttachmentUrl { get; set; } = null!;
        public string Upvotes { get; set; } = null!;
        public string Downvotes { get; set; } = null!;
        public IEnumerable<CityOption> LocationNames { get; set; } = new HashSet<CityOption>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<ApplicationUserSuggestion, MySuggestionsViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.SuggestionId.ToString()))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Suggestion.Title))
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Suggestion.Category))
                .ForMember(dest => dest.UploadedOn, opt => opt.MapFrom(src => src.Suggestion.UploadedOn.ToString("dd/MM/yyyy")))
                .ForMember(dest => dest.AttachmentUrl, opt => opt.MapFrom(src => src.Suggestion.AttachmentUrl))
                .ForMember(dest => dest.Upvotes, opt => opt.MapFrom(src => src.Suggestion.Upvotes.ToString()))
                .ForMember(dest => dest.Downvotes, opt => opt.MapFrom(src => src.Suggestion.Downvotes.ToString()));
        }
    }
}

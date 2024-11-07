using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Mapping;
using UrbanSystem.Web.ViewModels.Suggestions;
using UrbanSystem.Web.ViewModels.SuggestionsLocations;

namespace UrbanSystem.Web.ViewModels.Locations
{
    public class LocationDetailsViewModel : IMapFrom<Location>, IHaveCustomMappings
    {
        public string Id { get; set; } = null!;

        public string CityName { get; set; } = null!;

        public string StreetName { get; set; } = null!;

        public string CityPicture { get; set; } = null!;

        public IEnumerable<SuggestionLocationViewModel> Suggestions { get; set; } = new HashSet<SuggestionLocationViewModel>();

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Location, LocationDetailsViewModel>()
                .ForMember(d => d.Suggestions, x => x.MapFrom(s => s.SuggestionsLocations.Select(sl => sl.Suggestion)));
        }
    }
}

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Services.Mapping;

namespace UrbanSystem.Web.ViewModels.SuggestionsLocations
{
    public class SuggestionLocationViewModel : IMapFrom<Suggestion>
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string UploadedOn { get; set; } = null!;
    }
}

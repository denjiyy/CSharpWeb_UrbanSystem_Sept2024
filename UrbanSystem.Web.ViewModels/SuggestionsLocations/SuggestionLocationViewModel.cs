using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Web.ViewModels.SuggestionsLocations
{
    public class SuggestionLocationViewModel
    {
        public string Id { get; set; } = null!;

        public string Title { get; set; } = null!;

        public string UploadedOn { get; set; } = null!;
    }
}

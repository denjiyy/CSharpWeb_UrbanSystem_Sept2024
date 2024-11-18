using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrbanSystem.Data.Models;
using UrbanSystem.Web.ViewModels.Locations;

namespace UrbanSystem.Web.ViewModels.Suggestions
{
    public class MySuggestionsViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string UploadedOn { get; set; } = null!;
        public string? AttachmentUrl { get; set; } = null!;
        public string Upvotes { get; set; } = null!;
        public string Downvotes { get; set; } = null!;
        public IEnumerable<CityOption> LocationNames { get; set; } = new HashSet<CityOption>();
    }
}

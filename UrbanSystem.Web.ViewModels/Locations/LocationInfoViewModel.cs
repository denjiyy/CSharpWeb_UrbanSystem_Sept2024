using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Web.ViewModels.Locations
{
    public class LocationInfoViewModel
    {
        public string Id { get; set; } = null!;

        public string CityName { get; set; } = null!;

        public string StreetName { get; set; } = null!;

        public string CityPicture { get; set; } = null!;
    }
}

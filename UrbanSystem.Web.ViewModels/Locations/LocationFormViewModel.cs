using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UrbanSystem.Common.EntityValidationMessages.Location;

namespace UrbanSystem.Web.ViewModels.Locations
{
    public class LocationFormViewModel
    {
        [Required(ErrorMessage = CityNameRequiredMessage)]
        [StringLength(100, ErrorMessage = CityNameMaxLengthMessage)]
        public string CityName { get; set; } = null!;

        [Required(ErrorMessage = StreetNameRequiredMessage)]
        [StringLength(100, ErrorMessage = StreetNameMaxLengthMessage)]
        public string StreetName { get; set; } = null!;

        [Display(Name = CityPictureDisplayMessage)]
        [Required(ErrorMessage = CityPictureRequiredMessage)]
        [Url(ErrorMessage = UrlErrorMessage)]
        public string CityPicture { get; set; } = null!;
    }
}

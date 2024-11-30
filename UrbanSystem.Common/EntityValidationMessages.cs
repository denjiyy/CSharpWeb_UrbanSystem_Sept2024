using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Common
{
    public static class EntityValidationMessages
    {
        public static class Suggestion
        {
            public const string TitleRequiredMessage = "Suggestion title is required!";
            public const string CategoryRequiredMessage = "The category is required!";
            public const string CityNameRequiredMessage = "Please select the city name!";
            public const string DescriptionRequiredMessage = "Please enter what you want to change!";
            public const string PriorityRequiredMessage = "Please select the priority of the issue!";
            public const string StatusRequiredMessage = "Please select the status of the issue!";
            public const string StreetNameRequiredMessage = "Please enter the name of the street, where the issue is located!";
        }

        public static class Location
        {
            public const string CityNameRequiredMessage = "City name is required!";
            public const string CityNameMaxLengthMessage = "City name cannot exceed 30 characters!";
            public const string StreetNameRequiredMessage = "Street name is required!";
            public const string StreetNameMaxLengthMessage = "Street name cannot exceed 20 characters!";
            public const string CityPictureRequiredMessage = "City picture URL is required!";
            public const string CityPictureDisplayMessage = "City picture URL";
            public const string UrlErrorMessage = "Please enter a valid URL!";
        }
    }
}
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
    }
}

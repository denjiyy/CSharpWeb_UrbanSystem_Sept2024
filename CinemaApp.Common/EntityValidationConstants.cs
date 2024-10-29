using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace UrbanSystem.Common
{
    public static class EntityValidationConstants
    {
        public static class Suggestion
        {
            public const int TitleMaxLength = 50;
            public const int CategoryMaxLength = 20;
            public const int DescriptionMaxLength = 200;
        }

        public static class Comment
        {
            public const int ContentMaxLength = 100;
        }

        public static class Province
        {
            public const int CityNameMaxLength = 30;
            public const int StreetMaxLength = 40;
        }
    }
}

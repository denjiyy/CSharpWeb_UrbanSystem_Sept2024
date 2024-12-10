namespace UrbanSystem.Common
{
    public static class EntityValidationConstants
    {
        public static class Suggestion
        {
            public const int TitleMaxLength = 50;
            public const int TitleMinLength = 5;
            public const int CategoryMaxLength = 20;
            public const int CategoryMinLength = 2;
            public const int DescriptionMaxLength = 200;
            public const int DescriptionMinLength = 10;
        }

        public static class Location
        {
            public const int CityNameMaxLength = 30;
            public const int StreetNameMaxLength = 40;
        }
    }
}
namespace HealthyFood.Models.Common
{
    public static class ModelValidation
    {
        public const string NameLengthError = "Name must be between {2} and {1} symbols.";
        public const string EmptyFieldLengthError = "Please enter the field.";
        public const string IdDisplayName = "No.";

        public static class RecipeValidation
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;

            public const int DescriptionMinLength = 50;
            public const int DescriptionMaxLength = 20000;
            public const string DescriptionError = "Description must be between {2} and {1} symbols.";

            public const int IngredientsMinLength = 10;
            public const int IngredientsMaxLength = 20000;
            public const string IngredientsError = "Ingredients must be between {2} and {1} symbols.";

            public const int ImagePathMaxLength = 1000;

            public const int PortionsMinLength = 0;
            public const int PortionsMaxLength = 12;

            public const int PreparationTimeMinLength = 0;
            public const int PreparationTimeMaxLength = 180;

            public const int CookingTimeMinLength = 0;
            public const int CookingTimeMaxLength = 180;

            public const string PreparationTimeDisplayName = "Preparation Time";
            public const string CookingTimeDisplayName = "Cooking Time";
            public const string PortionsNumberDisplayName = "Portions Number";
        }
    }
}

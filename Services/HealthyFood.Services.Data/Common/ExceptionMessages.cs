﻿namespace HealthyFood.Services.Data.Common
{
    public static class ExceptionMessages
    {
        public const string RecipeAlreadyExists = "Recipe with name {0} already exists.";

        public const string RecipeNotFound = "Recipe with id {0} is not found.";

        public const string RecipeNameNotFound = "Recipe with name {0} is not found.";

        public const string CategoryNotFound = "Category with id {0} is not found.";

        public const string DifficultyInvalidType = "Difficulty type {0} is invalid";
    }
}
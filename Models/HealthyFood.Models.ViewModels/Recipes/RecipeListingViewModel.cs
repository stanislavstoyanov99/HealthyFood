namespace HealthyFood.Models.ViewModels.Recipes
{
    using System;

    using HealthyFood.Data.Models;
    using HealthyFood.Data.Models.Enumerations;
    using HealthyFood.Services.Mapping;

    public class RecipeListingViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImagePath { get; set; }

        public int Rate { get; set; }

        public Difficulty Difficulty { get; set; }

        public DateTime CreatedOn { get; set; }

        public Category Category { get; set; }
    }
}

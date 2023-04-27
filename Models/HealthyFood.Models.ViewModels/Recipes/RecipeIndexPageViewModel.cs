namespace HealthyFood.Models.ViewModels.Recipes
{
    using System.Collections.Generic;

    using HealthyFood.Models.ViewModels.Categories;

    public class RecipeIndexPageViewModel
    {
        public PaginatedList<RecipeListingViewModel> RecipesPaginated { get; set; }

        public IEnumerable<CategoryListingViewModel> Categories { get; set; }
    }
}

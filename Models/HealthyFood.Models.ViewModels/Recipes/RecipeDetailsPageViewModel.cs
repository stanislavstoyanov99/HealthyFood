namespace HealthyFood.Models.ViewModels.Recipes
{
    using HealthyFood.Models.ViewModels.Reviews;

    public class RecipeDetailsPageViewModel
    {
        public RecipeDetailsViewModel Recipe { get; set; }

        public CreateReviewInputModel CreateReviewInputModel { get; set; }
    }
}

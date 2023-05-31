namespace HealthyFood.Models.ViewModels.Home
{
    using System.Collections.Generic;
    using HealthyFood.Models.ViewModels.Articles;
    using HealthyFood.Models.ViewModels.Recipes;

    public class HomePageViewModel
    {
        public IEnumerable<ArticleListingViewModel> RecentArticles { get; set; }

        public IEnumerable<RecipeListingViewModel> TopRecipes { get; set; }

        public IEnumerable<List<GalleryViewModel>> SubGallery { get; set; }
    }
}

namespace HealthyFood.Web.Controllers
{
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Common;
    using HealthyFood.Common.Attributes;
    using HealthyFood.Models.ViewModels;
    using HealthyFood.Models.ViewModels.Articles;
    using HealthyFood.Models.ViewModels.Home;
    using HealthyFood.Models.ViewModels.Privacy;
    using HealthyFood.Models.ViewModels.Recipes;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        private const int TopRecipesCounter = 6;
        private const int RecentArticlesCounter = 2;

        private readonly IPrivacyService privacyService;
        private readonly IArticlesService articlesService;
        private readonly IRecipesService recipesService;

        public HomeController(
            IPrivacyService privacyService,
            IArticlesService articlesService,
            IRecipesService recipesService)
        {
            this.privacyService = privacyService;
            this.articlesService = articlesService;
            this.recipesService = recipesService;
        }

        public async Task<IActionResult> Index()
        {
            var topRecipes = await this
                .recipesService.GetTopRecipesAsync<RecipeListingViewModel>(TopRecipesCounter);

            var recentArticles = await this
                .articlesService.GetRecentArticlesAsync<ArticleListingViewModel>(RecentArticlesCounter);

            var gallery = await this
                .recipesService.GetAllRecipesAsync<GalleryViewModel>();

            var subGallery = gallery
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / 3)
                .Select(x => x.Select(v => v.Value).ToList());

            var model = new HomePageViewModel
            {
                TopRecipes = topRecipes,
                RecentArticles = recentArticles,
                SubGallery = subGallery,
            };

            return this.View(model);
        }

        [NoDirectAccessAttribute]
        public IActionResult ThankYouSubscription(string email)
        {
            return this.View("SuccessfullySubscribed", email);
        }

        [NoDirectAccessAttribute]
        public IActionResult RequestNewPassword(ChangePasswordViewModel model)
        {
            return this.View(model);
        }

        public async Task<IActionResult> Privacy()
        {
            var privacy = await this.privacyService.GetViewModelAsync<PrivacyDetailsViewModel>();

            return this.View(privacy);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public IActionResult HttpError(HttpErrorViewModel errorViewModel)
        {
            if (errorViewModel.StatusCode == 404)
            {
                return this.View(errorViewModel);
            }

            return this.Error();
        }
    }
}

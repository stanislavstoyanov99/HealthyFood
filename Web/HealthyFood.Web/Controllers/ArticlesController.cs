namespace HealthyFood.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Common.Attributes;
    using HealthyFood.Models.ViewModels;
    using HealthyFood.Models.ViewModels.Articles;
    using HealthyFood.Models.ViewModels.Categories;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class ArticlesController : Controller
    {
        private const int PageSize = 9;
        private const int RecentArticlesCount = 6;
        private const int ArticlesByCategoryCount = 6;
        private const int ArticlesInSearchPage = 4;
        private readonly IArticlesService articlesService;
        private readonly ICategoriesService categoriesService;

        public ArticlesController(
            IArticlesService articlesService,
            ICategoriesService categoriesService)
        {
            this.articlesService = articlesService;
            this.categoriesService = categoriesService;
        }

        [ServiceFilter(typeof(PasswordExpirationCheckAttribute))]
        public async Task<IActionResult> Index(int? pageNumber)
        {
            var allArticles = await Task.Run(() =>
                this.articlesService.GetAllArticlesAsQueryable<ArticleListingViewModel>());

            var model = await PaginatedList<ArticleListingViewModel>.CreateAsync(
                allArticles, pageNumber ?? 1, PageSize);

            return this.View(model);
        }

        [ServiceFilter(typeof(PasswordExpirationCheckAttribute))]
        public async Task<IActionResult> Details(int id)
        {
            var article = await this.articlesService
                .GetViewModelByIdAsync<ArticleListingViewModel>(id);

            var categories = await this.categoriesService
                .GetAllCategoriesAsync<CategoryListingViewModel>();

            var recentArticles = await this.articlesService
                .GetRecentArticlesAsync<RecentArticleListingViewModel>(RecentArticlesCount);

            var viewModel = new DetailsListingViewModel
            {
                ArticleListingViewModel = article,
                Categories = categories,
                RecentArticles = recentArticles,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> Search(int? pageNumber, string searchTitle)
        {
            if (string.IsNullOrEmpty(searchTitle))
            {
                return this.NotFound();
            }

            var articles = await Task.Run(() => this.articlesService
                .GetAllArticlesAsQueryable<ArticleListingViewModel>()
                .Where(x => x.Title.ToLower().Contains(searchTitle.ToLower())));

            if (!articles.Any())
            {
                return this.NotFound();
            }

            this.TempData["SearchTitle"] = searchTitle;

            var articlesPaginated = await PaginatedList<ArticleListingViewModel>
                .CreateAsync(articles, pageNumber ?? 1, ArticlesInSearchPage);

            return this.View(articlesPaginated);
        }

        public async Task<IActionResult> ByName(int? pageNumber, string categoryName)
        {
            var articlesByCategoryName = await Task.Run(() => this.articlesService
                .GetAllArticlesByCategoryNameAsQueryable<ArticleListingViewModel>(categoryName));

            if (!articlesByCategoryName.Any())
            {
                return this.NotFound();
            }

            this.TempData["CategoryName"] = categoryName;
            var articlesByCategoryNamePaginated = await PaginatedList<ArticleListingViewModel>
                .CreateAsync(articlesByCategoryName, pageNumber ?? 1, ArticlesByCategoryCount);

            return this.View(articlesByCategoryNamePaginated);
        }
    }
}

namespace HealthyFood.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using HealthyFood.Data.Models;
    using HealthyFood.Models.ViewModels.Articles;
    using HealthyFood.Models.ViewModels.Categories;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ArticleCommentsController : Controller
    {
        private readonly IArticleCommentsSertvice articleCommentsSertvice;
        private readonly IArticlesService articlesService;
        private readonly ICategoriesService categoriesService;
        private UserManager<ApplicationUser> userManager;

        public ArticleCommentsController(
            IArticleCommentsSertvice articleCommentsSertvice,
            IArticlesService articlesService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.articleCommentsSertvice = articleCommentsSertvice;
            this.articlesService = articlesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(DetailsListingViewModel viewModel)
        {
            if (!this.ModelState.IsValid)
            {
                var article = await this.articlesService.GetViewModelByIdAsync<ArticleListingViewModel>(
                    viewModel.CreateArticleCommentInputModel.ArticleId);

                var categories = await this.categoriesService
                    .GetAllCategoriesAsync<CategoryListingViewModel>();

                var recentArticles = await this.articlesService
                    .GetRecentArticlesAsync<RecentArticleListingViewModel>(6);

                var model = new DetailsListingViewModel
                {
                    ArticleListingViewModel = article,
                    Categories = categories,
                    RecentArticles = recentArticles,
                    CreateArticleCommentInputModel = viewModel.CreateArticleCommentInputModel,
                };

                return this.View("/Views/Articles/Details.cshtml", model);
            }

            var parentId = viewModel.CreateArticleCommentInputModel.ParentId == 0
                ? (int?)null
                : viewModel.CreateArticleCommentInputModel.ParentId;

            if (parentId.HasValue)
            {
                var isInArticle = await this.articleCommentsSertvice.IsInArticleId(
                    parentId.Value, viewModel.CreateArticleCommentInputModel.ArticleId);

                if (!isInArticle)
                {
                    return this.BadRequest();
                }
            }

            var userId = this.userManager.GetUserId(this.User);

            try
            {
                await this.articleCommentsSertvice.CreateAsync(
                    viewModel.CreateArticleCommentInputModel.ArticleId,
                    userId,
                    viewModel.CreateArticleCommentInputModel.Content.Trim(),
                    parentId);
            }
            catch (ArgumentException ex)
            {
                return this.BadRequest(ex.Message);
            }

            return this.RedirectToAction(
                "Details", "Articles", new { id = viewModel.CreateArticleCommentInputModel.ArticleId });
        }
    }
}

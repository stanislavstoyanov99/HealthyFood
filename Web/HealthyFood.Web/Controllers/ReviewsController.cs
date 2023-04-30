namespace HealthyFood.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using HealthyFood.Data.Models;
    using HealthyFood.Models.ViewModels.Recipes;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class ReviewsController : Controller
    {
        private readonly IReviewsService reviewsService;
        private readonly IRecipesService recipesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ReviewsController(
            IReviewsService reviewsService,
            IRecipesService recipesService,
            UserManager<ApplicationUser> userManager)
        {
            this.reviewsService = reviewsService;
            this.recipesService = recipesService;
            this.userManager = userManager;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(RecipeDetailsPageViewModel input)
        {
            if (!this.ModelState.IsValid)
            {
                var recipe = await this.recipesService
                    .GetViewModelByIdAsync<RecipeDetailsViewModel>(input.Recipe.Id);

                var model = new RecipeDetailsPageViewModel
                {
                    Recipe = recipe,
                    CreateReviewInputModel = input.CreateReviewInputModel,
                };

                return this.View("/Views/Recipes/Details.cshtml", model);
            }

            var userId = this.userManager.GetUserId(this.User);
            input.CreateReviewInputModel.UserId = userId;

            try
            {
                await this.reviewsService.CreateAsync(input.CreateReviewInputModel);
            }
            catch (ArgumentException aex)
            {
                return this.BadRequest(aex.Message);
            }

            return this.RedirectToAction("Details", "Recipes", new { id = input.CreateReviewInputModel.RecipeId });
        }

        public async Task<IActionResult> Remove(int reviewId, int recipeId)
        {
            await this.reviewsService.DeleteByIdAsync(reviewId);

            return this.RedirectToAction("Details", "Recipes", new { id = recipeId });
        }
    }
}

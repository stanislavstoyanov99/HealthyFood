namespace HealthyFood.Web.Controllers
{
    using System.Threading.Tasks;

    using HealthyFood.Common.Attributes;
    using HealthyFood.Data.Models;
    using HealthyFood.Models.InputModels.AdministratorInputModels.Recipes;
    using HealthyFood.Models.ViewModels;
    using HealthyFood.Models.ViewModels.Categories;
    using HealthyFood.Models.ViewModels.Recipes;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class RecipesController : Controller
    {
        private const int PageSize = 12;
        private readonly IRecipesService recipesService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public RecipesController(
            IRecipesService recipesService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.recipesService = recipesService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index(string categoryName, int? pageNumber)
        {
            this.TempData["CategoryName"] = categoryName;

            var recipes = this.recipesService
                .GetAllRecipesByFilterAsQueryeable<RecipeListingViewModel>(categoryName);

            var recipesPaginated = await PaginatedList<RecipeListingViewModel>
                .CreateAsync(recipes, pageNumber ?? 1, PageSize);

            var categories = await this.categoriesService
                .GetAllCategoriesAsync<CategoryListingViewModel>();

            var model = new RecipeIndexPageViewModel
            {
                RecipesPaginated = recipesPaginated,
                Categories = categories,
            };

            return this.View(model);
        }

        public async Task<IActionResult> Details(int id)
        {
            var recipe = await this.recipesService.GetViewModelByIdAsync<RecipeDetailsViewModel>(id);

            var model = new RecipeDetailsPageViewModel
            {
                Recipe = recipe,
            };

            return this.View(model);
        }

        public async Task<IActionResult> RecipeList()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var recipes = await this.recipesService.GetAllRecipesByUserId<RecipeDetailsViewModel>(user.Id);
            var categories = await this.categoriesService.GetAllCategoriesAsync<CategoryDetailsViewModel>();

            var model = new MyRecipeDetailsViewModel
            {
                Recipes = recipes,
                RecipeEditViewModel = new RecipeEditViewModel { Categories = categories, },
            };

            return this.View(model);
        }

        public async Task<IActionResult> Submit()
        {
            var categories = await this.categoriesService
                .GetAllCategoriesAsync<CategoryDetailsViewModel>();

            var model = new RecipeCreateInputModel
            {
                Categories = categories,
            };

            return this.View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Submit(RecipeCreateInputModel recipeCreateInputModel)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            if (!this.ModelState.IsValid)
            {
                var categories = await this.categoriesService
                  .GetAllCategoriesAsync<CategoryDetailsViewModel>();

                recipeCreateInputModel.Categories = categories;

                return this.View(recipeCreateInputModel);
            }

            var recipe = await this.recipesService.CreateAsync(recipeCreateInputModel, user.Id);
            return this.RedirectToAction("Details", "Recipes", new { id = recipe.Id });
        }

        [Authorize]
        public async Task<IActionResult> Remove(int id)
        {
            await this.recipesService.DeleteByIdAsync(id);

            return this.RedirectToAction("RecipeList", "Recipes");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(MyRecipeDetailsViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model.RecipeEditViewModel);
            }

            await this.recipesService.EditAsync(model.RecipeEditViewModel);
            return this.RedirectToAction("RecipeList", "Recipes");
        }

        [HttpGet]
        public async Task<RecipeDetailsViewModel> GetRecipe(int id)
        {
            var recipe = await this.recipesService.GetViewModelByIdAsync<RecipeDetailsViewModel>(id);
            return recipe;
        }
    }
}

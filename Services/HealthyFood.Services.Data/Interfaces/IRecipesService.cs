namespace HealthyFood.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Models.InputModels.AdministratorInputModels.Recipes;
    using HealthyFood.Models.ViewModels.Recipes;

    public interface IRecipesService : IBaseDataService
    {
        Task<RecipeDetailsViewModel> CreateAsync(RecipeCreateInputModel recipeCreateInputModel, string userId);

        Task EditAsync(RecipeEditViewModel recipeEditViewModel);

        IQueryable<TViewModel> GetAllRecipesAsQueryeable<TViewModel>();

        Task<IEnumerable<TViewModel>> GetTopRecipesAsync<TViewModel>(int count = 0);

        Task<IEnumerable<TViewModel>> GetAllRecipesAsync<TViewModel>();

        IQueryable<TViewModel> GetAllRecipesByFilterAsQueryeable<TViewModel>(string categoryName = null);

        Task<IEnumerable<TViewModel>> GetAllRecipesByUserId<TViewModel>(string userId);
    }
}

namespace HealthyFood.Services.Data.Interfaces
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Models.InputModels.AdministratorInputModels.Recipes;
    using HealthyFood.Models.ViewModels.Recipes;

    public interface IRecipesService : IBaseDataService
    {
        Task<RecipeDetailsViewModel> CreateAsync(RecipeCreateInputModel recipeCreateInputModel, string userId);

        Task EditAsync(RecipeEditViewModel recipeEditViewModel);

        IQueryable<TViewModel> GetAllRecipesAsQueryeable<TViewModel>();
    }
}

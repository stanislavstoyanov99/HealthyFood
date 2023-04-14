namespace HealthyFood.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthyFood.Models.InputModels.AdministratorInputModels.Categories;
    using HealthyFood.Models.ViewModels.Categories;

    public interface ICategoriesService : IBaseDataService
    {
        Task<CategoryDetailsViewModel> CreateAsync(CategoryCreateInputModel categoryCreateInputModel);

        Task EditAsync(CategoryEditViewModel categoryEditViewModel);

        Task<IEnumerable<TEntity>> GetAllCategoriesAsync<TEntity>();

        Task<TViewModel> GetCategoryAsync<TViewModel>(string name);
    }
}

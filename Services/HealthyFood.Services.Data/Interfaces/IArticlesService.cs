namespace HealthyFood.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Models.InputModels.AdministratorInputModels.Articles;
    using HealthyFood.Models.ViewModels.Articles;

    public interface IArticlesService : IBaseDataService
    {
        Task<ArticleDetailsViewModel> CreateAsync(ArticleCreateInputModel articleCreateInputModel, string userId);

        Task EditAsync(ArticleEditViewModel articleEditViewModel, string userId);

        Task<IEnumerable<TViewModel>> GetAllArticlesAsync<TViewModel>();

        IQueryable<TViewModel> GetAllArticlesByCategoryNameAsQueryable<TViewModel>(string categoryName);

        IQueryable<TViewModel> GetAllArticlesAsQueryable<TViewModel>();

        Task<IEnumerable<TViewModel>> GetRecentArticlesAsync<TViewModel>(int count = 0);
    }
}

namespace HealthyFood.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthyFood.Models.InputModels.AdministratorInputModels.Faq;
    using HealthyFood.Models.ViewModels.Faq;

    public interface IFaqService : IBaseDataService
    {
        Task<FaqDetailsViewModel> CreateAsync(FaqCreateInputModel faqCreateInputModel);

        Task EditAsync(FaqEditViewModel faqEditViewModel);

        Task<IEnumerable<TEntity>> GetAllFaqsAsync<TEntity>();
    }
}

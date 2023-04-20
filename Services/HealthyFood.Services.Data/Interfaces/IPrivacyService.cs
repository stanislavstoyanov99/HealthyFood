namespace HealthyFood.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using HealthyFood.Models.InputModels.AdministratorInputModels.Privacy;
    using HealthyFood.Models.ViewModels.Privacy;

    public interface IPrivacyService : IBaseDataService
    {
        Task<PrivacyDetailsViewModel> CreateAsync(PrivacyCreateInputModel privacyCreateInputModel);

        Task EditAsync(PrivacyEditViewModel privacyEditViewModel);

        Task<TViewModel> GetViewModelAsync<TViewModel>();
    }
}

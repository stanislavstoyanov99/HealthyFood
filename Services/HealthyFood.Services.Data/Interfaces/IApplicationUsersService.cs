namespace HealthyFood.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IApplicationUsersService
    {
        Task BanByIdAsync(string id);

        Task UnbanByIdAsync(string id);

        Task<IEnumerable<TViewModel>> GetAllApplicationUsersAsync<TViewModel>();

        Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id);
    }
}

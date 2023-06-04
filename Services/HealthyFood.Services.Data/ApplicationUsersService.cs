namespace HealthyFood.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Data.Common.Repositories;
    using HealthyFood.Data.Models;
    using HealthyFood.Services.Data.Common;
    using HealthyFood.Services.Data.Interfaces;
    using HealthyFood.Services.Mapping;
    using Microsoft.EntityFrameworkCore;

    public class ApplicationUsersService : IApplicationUsersService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> applicationUsersRepository;

        public ApplicationUsersService(IDeletableEntityRepository<ApplicationUser> applicationUsersRepository)
        {
            this.applicationUsersRepository = applicationUsersRepository;
        }

        public async Task BanByIdAsync(string id)
        {
            var healthyFoodUser = await this.applicationUsersRepository
                .All()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (healthyFoodUser == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ApplicationUserNotFound, id));
            }

            this.applicationUsersRepository.Delete(healthyFoodUser);
            await this.applicationUsersRepository.SaveChangesAsync();
        }

        public async Task UnbanByIdAsync(string id)
        {
            var healthyFoodUser = await this.applicationUsersRepository
                .AllWithDeleted()
                .FirstOrDefaultAsync(u => u.Id == id);

            if (healthyFoodUser == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ApplicationUserNotFound, id));
            }

            this.applicationUsersRepository.Undelete(healthyFoodUser);
            await this.applicationUsersRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TViewModel>> GetAllApplicationUsersAsync<TViewModel>()
        {
            var users = await this.applicationUsersRepository
              .AllWithDeleted()
              .To<TViewModel>()
              .ToListAsync();

            return users;
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(string id)
        {
            var healthyFoodUserViewModel = await this.applicationUsersRepository
                .AllWithDeleted()
                .Where(u => u.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            if (healthyFoodUserViewModel == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.ApplicationUserNotFound, id));
            }

            return healthyFoodUserViewModel;
        }
    }
}

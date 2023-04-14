namespace HealthyFood.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Data.Common.Repositories;
    using HealthyFood.Data.Models;
    using HealthyFood.Models.InputModels.AdministratorInputModels.Categories;
    using HealthyFood.Models.ViewModels.Categories;
    using HealthyFood.Services.Data.Common;
    using HealthyFood.Services.Data.Interfaces;
    using HealthyFood.Services.Mapping;

    using Microsoft.EntityFrameworkCore;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<CategoryDetailsViewModel> CreateAsync(CategoryCreateInputModel categoryCreateInputModel)
        {
            var category = new Category
            {
                Name = categoryCreateInputModel.Name,
                Description = categoryCreateInputModel.Description,
            };

            var doesCategoryExist = await this.categoriesRepository
                .All()
                .AnyAsync(x => x.Name == category.Name);

            if (doesCategoryExist)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.CategoryAlreadyExists, category.Name));
            }

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();

            var viewModel = await this.GetViewModelByIdAsync<CategoryDetailsViewModel>(category.Id);

            return viewModel;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var category = await this.categoriesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.CategoryNotFound, id));
            }

            this.categoriesRepository.Delete(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(CategoryEditViewModel categoryEditViewModel)
        {
            var category = await this.categoriesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == categoryEditViewModel.Id);

            if (category == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.CategoryNotFound, categoryEditViewModel.Id));
            }

            category.Name = categoryEditViewModel.Name;
            category.Description = categoryEditViewModel.Description;

            this.categoriesRepository.Update(category);
            await this.categoriesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllCategoriesAsync<TEntity>()
        {
            return await this.categoriesRepository
                .All()
                .OrderBy(x => x.Name)
                .To<TEntity>()
                .ToListAsync();
        }

        public async Task<TViewModel> GetCategoryAsync<TViewModel>(string name)
        {
            return await this.categoriesRepository
                .All()
                .Where(x => x.Name == name)
                .To<TViewModel>()
                .FirstOrDefaultAsync();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var categoryViewModel = await this.categoriesRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            if (categoryViewModel == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.CategoryNotFound, id));
            }

            return categoryViewModel;
        }
    }
}

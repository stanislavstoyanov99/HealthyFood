namespace HealthyFood.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Data.Common.Repositories;
    using HealthyFood.Data.Models;
    using HealthyFood.Models.InputModels.AdministratorInputModels.Articles;
    using HealthyFood.Models.ViewModels.Articles;
    using HealthyFood.Services.Data.Common;
    using HealthyFood.Services.Data.Interfaces;
    using HealthyFood.Services.Mapping;

    using Microsoft.EntityFrameworkCore;

    public class ArticlesService : IArticlesService
    {
        private readonly IDeletableEntityRepository<Article> articlesRepository;
        private readonly IDeletableEntityRepository<Category> categoriesRepository;
        private readonly ICloudinaryService cloudinaryService;

        public ArticlesService(
            IDeletableEntityRepository<Article> articlesRepository,
            IDeletableEntityRepository<Category> categoriesRepository,
            ICloudinaryService cloudinaryService)
        {
            this.articlesRepository = articlesRepository;
            this.categoriesRepository = categoriesRepository;
            this.cloudinaryService = cloudinaryService;
        }

        public async Task<ArticleDetailsViewModel> CreateAsync(
            ArticleCreateInputModel articleCreateInputModel, string userId)
        {
            var category = await this.categoriesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == articleCreateInputModel.CategoryId);

            if (category == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.CategoryNotFound, articleCreateInputModel.CategoryId));
            }

            var article = new Article
            {
                Title = articleCreateInputModel.Title,
                Description = articleCreateInputModel.Description,
                Category = category,
                UserId = userId,
            };

            var doesArticleExist = await this.articlesRepository
                .All()
                .AnyAsync(x => x.Title == article.Title);

            if (doesArticleExist)
            {
                throw new ArgumentException(
                    string.Format(ExceptionMessages.ArticleAlreadyExists, article.Title));
            }

            var imageUrl = await this.cloudinaryService
               .UploadAsync(articleCreateInputModel.Image, articleCreateInputModel.Title + Suffixes.ArticleSuffix);
            article.ImagePath = imageUrl;
            await this.articlesRepository.AddAsync(article);
            await this.articlesRepository.SaveChangesAsync();

            var viewModel = await this.GetViewModelByIdAsync<ArticleDetailsViewModel>(article.Id);

            return viewModel;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var article = await this.articlesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (article == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ArticleNotFound, id));
            }

            this.articlesRepository.Delete(article);
            await this.articlesRepository.SaveChangesAsync();
        }

        public async Task EditAsync(ArticleEditViewModel articleEditViewModel, string userId)
        {
            var article = await this.articlesRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == articleEditViewModel.Id);

            if (article == null)
            {
                throw new NullReferenceException(
                    string.Format(ExceptionMessages.ArticleNotFound, articleEditViewModel.Id));
            }

            if (articleEditViewModel.Image != null)
            {
                var newImageUrl = await this.cloudinaryService
                    .UploadAsync(articleEditViewModel.Image, articleEditViewModel.Title + Suffixes.ArticleSuffix);
                article.ImagePath = newImageUrl;
            }

            article.Title = articleEditViewModel.Title;
            article.Description = articleEditViewModel.Description;
            article.UserId = userId;
            article.CategoryId = articleEditViewModel.CategoryId;

            this.articlesRepository.Update(article);
            await this.articlesRepository.SaveChangesAsync();
        }

        public IQueryable<TViewModel> GetAllArticlesAsQueryable<TViewModel>()
        {
            return this.articlesRepository
                .All()
                .OrderBy(x => x.Title)
                .ThenByDescending(x => x.CreatedOn)
                .To<TViewModel>();
        }

        public async Task<IEnumerable<TViewModel>> GetAllArticlesAsync<TViewModel>()
        {
            return await this.articlesRepository
                .All()
                .OrderBy(x => x.Title)
                .To<TViewModel>()
                .ToListAsync();
        }

        public IQueryable<TViewModel> GetAllArticlesByCategoryNameAsQueryable<TViewModel>(string categoryName)
        {
            return this.articlesRepository
                .All()
                .Where(x => x.Category.Name == categoryName)
                .OrderBy(x => x.Title)
                .To<TViewModel>();
        }

        public async Task<IEnumerable<TViewModel>> GetRecentArticlesAsync<TViewModel>(int count = 0)
        {
            return await this.articlesRepository
                .All()
                .OrderByDescending(x => x.CreatedOn)
                .Take(count)
                .To<TViewModel>()
                .ToListAsync();
        }

        public async Task<TViewModel> GetViewModelByIdAsync<TViewModel>(int id)
        {
            var articlesViewModel = await this.articlesRepository
                .All()
                .Where(x => x.Id == id)
                .To<TViewModel>()
                .FirstOrDefaultAsync();

            if (articlesViewModel == null)
            {
                throw new NullReferenceException(string.Format(ExceptionMessages.ArticleNotFound, id));
            }

            return articlesViewModel;
        }
    }
}

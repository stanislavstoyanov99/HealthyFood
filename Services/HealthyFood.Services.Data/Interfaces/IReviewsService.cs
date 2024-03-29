﻿namespace HealthyFood.Services.Data.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using HealthyFood.Models.ViewModels.Reviews;

    public interface IReviewsService : IBaseDataService
    {
        public Task CreateAsync(CreateReviewInputModel createReviewInputModel);

        public Task<IEnumerable<TViewModel>> GetAll<TViewModel>(int recipeId);

        public Task<IEnumerable<TViewModel>> GetTopReviews<TViewModel>();
    }
}

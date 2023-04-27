namespace HealthyFood.Models.ViewModels.Categories
{
    using System.Collections.Generic;

    using HealthyFood.Data.Models;
    using HealthyFood.Models.ViewModels.Articles;
    using HealthyFood.Services.Mapping;

    public class CategoryListingViewModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<RecentArticleListingViewModel> Articles { get; set; }
    }
}

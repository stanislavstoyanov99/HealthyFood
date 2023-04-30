namespace HealthyFood.Web.Areas.Administration.Controllers
{
    using System.Linq;

    using HealthyFood.Data.Common.Repositories;
    using HealthyFood.Data.Models;
    using HealthyFood.Models.ViewModels.AdminDashboard;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class StatisticsController : ControllerBase
    {
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;
        private readonly IDeletableEntityRepository<Recipe> recipesRepository;
        private readonly IDeletableEntityRepository<Article> articlesRepository;
        private readonly IDeletableEntityRepository<Review> reviewsRepository;
        private readonly IDeletableEntityRepository<ArticleComment> commentsRepository;

        public StatisticsController(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Article> articlesRepository,
            IDeletableEntityRepository<Review> reviewsRepository,
            IDeletableEntityRepository<ArticleComment> commentsRepository)
        {
            this.usersRepository = usersRepository;
            this.recipesRepository = recipesRepository;
            this.articlesRepository = articlesRepository;
            this.reviewsRepository = reviewsRepository;
            this.commentsRepository = commentsRepository;
        }

        [HttpGet]
        public ActionResult<StatisticsResponseModel> Get()
        {
            var registeredUsers = new int[12];

            for (var i = 0; i < registeredUsers.Length; ++i)
            {
                registeredUsers[i] = this.usersRepository
                    .All()
                    .Count(x => x.CreatedOn.Month == i + 1);
            }

            var model = new StatisticsResponseModel
            {
                recipesCount = this.recipesRepository.All().Count(),
                articlesCount = this.articlesRepository.All().Count(),
                reviewedrecipesCount = this.recipesRepository.All().Count(x => x.Reviews.Count >= 0),
                reviewsCount = this.reviewsRepository.All().Count(),
                commentsCount = this.commentsRepository.All().Count(),
                registeredUsers = registeredUsers,
            };

            return model;
        }
    }
}

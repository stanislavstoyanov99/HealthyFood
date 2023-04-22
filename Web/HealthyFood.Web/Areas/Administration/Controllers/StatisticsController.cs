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
        private readonly IDeletableEntityRepository<ArticleComment> commentRepository;

        public StatisticsController(
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            IDeletableEntityRepository<Recipe> recipesRepository,
            IDeletableEntityRepository<Article> articlesRepository,
            IDeletableEntityRepository<Review> reviewsRepository,
            IDeletableEntityRepository<ArticleComment> commentRepository)
        {
            this.usersRepository = usersRepository;
            this.recipesRepository = recipesRepository;
            this.articlesRepository = articlesRepository;
            this.reviewsRepository = reviewsRepository;
            this.commentRepository = commentRepository;
        }

        [HttpGet]
        public ActionResult<StatisticsResponseModel> Get()
        {
            var registeredUsersArray = new int[12];

            for (var i = 0; i < registeredUsersArray.Length; ++i)
            {
                registeredUsersArray[i] = this.usersRepository
                    .All()
                    .Count(x => x.CreatedOn.Month == i + 1);
            }

            var model = new StatisticsResponseModel
            {
                recipesCount = this.recipesRepository.All().Count(),
                articlesCount = this.articlesRepository.All().Count(),
                reviewedrecipesCount = this.recipesRepository.All().Count(x => x.Reviews.Count >= 0),
                reviewsCount = this.reviewsRepository.All().Count(),
                commentsCount = this.commentRepository.All().Count(),
                registeredUsers = registeredUsersArray,
            };

            return model;
        }
    }
}

namespace HealthyFood.Web.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using HealthyFood.Models.ViewModels;
    using HealthyFood.Models.ViewModels.Articles;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class ArticlesSearchController : ControllerBase
    {
        private const int ArticlesInSearchPage = 4;
        private readonly IArticlesService articlesService;

        public ArticlesSearchController(IArticlesService articlesService)
        {
            this.articlesService = articlesService;
        }

        [HttpGet("{pageNumber}/{searchTitle}")]
        public async Task<ActionResult<PaginatedList<ArticleListingViewModel>>> Get(
            [FromRoute] int? pageNumber,
            [FromRoute] string searchTitle)
        {
            var articles = await Task.Run(() => this.articlesService
               .GetAllArticlesAsQueryable<ArticleListingViewModel>()
               .Where(a => a.Title.ToLower().Contains(searchTitle.ToLower())));

            if (articles.Count() == 0)
            {
                return this.NotFound();
            }

            var articlesPaginated = await PaginatedList<ArticleListingViewModel>
                .CreateAsync(articles, pageNumber ?? 1, ArticlesInSearchPage);

            return articlesPaginated;
        }
    }
}

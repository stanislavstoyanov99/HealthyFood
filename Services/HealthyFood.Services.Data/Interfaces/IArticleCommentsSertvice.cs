namespace HealthyFood.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    public interface IArticleCommentsSertvice
    {
        Task CreateAsync(int articleId, string userId, string content, int? parentId = null);

        Task<bool> IsInArticleId(int commentId, int articleId);
    }
}

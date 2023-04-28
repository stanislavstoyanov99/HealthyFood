namespace HealthyFood.Models.ViewModels.ArticleComments
{
    using System.ComponentModel.DataAnnotations;

    using static HealthyFood.Models.Common.ModelValidation;

    public class CreateArticleCommentInputModel
    {
        public int ArticleId { get; set; }

        public int ParentId { get; set; }

        [Required(ErrorMessage = EmptyFieldLengthError)]
        public string Content { get; set; }
    }
}

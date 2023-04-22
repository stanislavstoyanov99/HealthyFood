namespace HealthyFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Common.Models;

    using static HealthyFood.Data.Common.DataValidation.ArticleCommentValidation;

    public class ArticleComment : BaseDeletableModel<int>
    {
        public int ArticleId { get; set; }

        public virtual Article Article { get; set; }

        public int? ParentId { get; set; }

        public virtual ArticleComment Parent { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

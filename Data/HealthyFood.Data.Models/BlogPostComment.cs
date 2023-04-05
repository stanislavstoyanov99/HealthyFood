namespace HealthyFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Common.Models;

    using static HealthyFood.Data.Common.DataValidation.BlogPostCommentValidation;

    public class BlogPostComment : BaseDeletableModel<int>
    {
        public int BlogPostId { get; set; }

        public virtual BlogPost BlogPost { get; set; }

        public int? ParentId { get; set; }

        public virtual BlogPostComment Parent { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

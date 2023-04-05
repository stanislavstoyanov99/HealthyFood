namespace HealthyFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Common.Models;

    using static HealthyFood.Data.Common.DataValidation.ReviewCommentValidation;

    public class ReviewComment : BaseDeletableModel<int>
    {
        public int ReviewId { get; set; }

        public virtual Review Review { get; set; }

        public int? ParentId { get; set; }

        public virtual ReviewComment Parent { get; set; }

        [Required]
        [MaxLength(ContentMaxLength)]
        public string Content { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}

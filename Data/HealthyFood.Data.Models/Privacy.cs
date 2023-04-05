namespace HealthyFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Common.Models;

    using static HealthyFood.Data.Common.DataValidation.PrivacyValidation;

    public class Privacy : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(ContentPageMaxLength)]
        public string PageContent { get; set; }
    }
}

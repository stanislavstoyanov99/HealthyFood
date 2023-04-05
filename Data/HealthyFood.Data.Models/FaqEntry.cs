namespace HealthyFood.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Common.Models;

    using static HealthyFood.Data.Common.DataValidation.FaqEntryValidation;

    public class FaqEntry : BaseDeletableModel<int>
    {
        [Required]
        [MaxLength(QuestionMaxLength)]
        public string Question { get; set; }

        [Required]
        [MaxLength(AnswerMaxLength)]
        public string Answer { get; set; }
    }
}

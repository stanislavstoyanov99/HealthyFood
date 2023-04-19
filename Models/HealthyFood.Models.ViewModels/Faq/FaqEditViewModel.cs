namespace HealthyFood.Models.ViewModels.Faq
{
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Models;
    using HealthyFood.Services.Mapping;

    using static HealthyFood.Models.Common.ModelValidation;
    using static HealthyFood.Models.Common.ModelValidation.FaqEntryValidation;

    public class FaqEditViewModel : IMapFrom<FaqEntry>
    {
        public int Id { get; set; }

        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(QuestionMaxLength, MinimumLength = QuestionMinLength, ErrorMessage = QuestionLengthError)]
        public string Question { get; set; }

        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(AnswerMaxLength, MinimumLength = AnswerMinLength, ErrorMessage = AnswerLengthError)]
        public string Answer { get; set; }
    }
}

namespace HealthyFood.Models.InputModels.AdministratorInputModels.Faq
{
    using System.ComponentModel.DataAnnotations;

    using static HealthyFood.Models.Common.ModelValidation;
    using static HealthyFood.Models.Common.ModelValidation.FaqEntryValidation;

    public class FaqCreateInputModel
    {
        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(QuestionMaxLength, MinimumLength = QuestionMinLength, ErrorMessage = QuestionLengthError)]
        public string Question { get; set; }

        [Required(ErrorMessage = EmptyFieldLengthError)]
        [StringLength(AnswerMaxLength, MinimumLength = AnswerMinLength, ErrorMessage = AnswerLengthError)]
        public string Answer { get; set; }
    }
}

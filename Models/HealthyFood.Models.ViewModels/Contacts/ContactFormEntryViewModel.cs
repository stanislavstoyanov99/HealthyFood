namespace HealthyFood.Models.ViewModels.Contacts
{
    using HealthyFood.Common.Attributes;
    using System.ComponentModel.DataAnnotations;

    using static HealthyFood.Models.Common.ModelValidation.ContactFormEntryValidation;

    public class ContactFormEntryViewModel
    {
        [Required(AllowEmptyStrings = false)]
        [StringLength(
            FirstNameMaxLength,
            MinimumLength = FirstNameMinLength,
            ErrorMessage = FirstNameLengthError)]
        public string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(
            LastNameMaxLength,
            MinimumLength = LastNameMinLength,
            ErrorMessage = LastNameLengthError)]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(
            SubjectMaxLength,
            MinimumLength = SubjectMinLength,
            ErrorMessage = SubjectLengthError)]
        public string Subject { get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(
            ContentMaxLength,
            MinimumLength = ContentMinLength,
            ErrorMessage = ContentLengthError)]
        public string Content { get; set; }

        [GoogleReCaptchaValidation]
        public string RecaptchaValue { get; set; }
    }
}

namespace HealthyFood.Models.ViewModels.Privacy
{
    using System.ComponentModel.DataAnnotations;
    using Ganss.Xss;

    using HealthyFood.Data.Models;
    using HealthyFood.Services.Mapping;

    using static HealthyFood.Models.Common.ModelValidation.PrivacyValidation;

    public class PrivacyDetailsViewModel : IMapFrom<Privacy>
    {
        public int Id { get; set; }

        [Display(Name = PageContentDisplayName)]
        public string PageContent { get; set; }

        public string SanitizedPageContent => new HtmlSanitizer().Sanitize(this.PageContent);
    }
}

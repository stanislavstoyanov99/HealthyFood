namespace HealthyFood.Models.ViewModels.Faq
{
    using Ganss.Xss;

    using HealthyFood.Data.Models;
    using HealthyFood.Services.Mapping;

    public class FaqDetailsViewModel : IMapFrom<FaqEntry>
    {
        public int Id { get; set; }

        public string Question { get; set; }

        public string Answer { get; set; }

        public string SanitizedAnswer => new HtmlSanitizer().Sanitize(this.Answer);
    }
}

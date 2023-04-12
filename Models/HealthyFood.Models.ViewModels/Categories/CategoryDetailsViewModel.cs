namespace HealthyFood.Models.ViewModels.Categories
{
    using System.ComponentModel.DataAnnotations;
    using Ganss.Xss;
    using HealthyFood.Data.Models;
    using HealthyFood.Services.Mapping;

    using static HealthyFood.Models.Common.ModelValidation;

    public class CategoryDetailsViewModel : IMapFrom<Category>
    {
        [Display(Name = IdDisplayName)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ShortDescription
        {
            get
            {
                var shortDescription = this.Description;
                return shortDescription.Length > 200
                        ? shortDescription.Substring(0, 200) + " ..."
                        : shortDescription;
            }
        }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string SanitizedShortDescription => new HtmlSanitizer().Sanitize(this.ShortDescription);

        public string UserUsername { get; set; }
    }
}

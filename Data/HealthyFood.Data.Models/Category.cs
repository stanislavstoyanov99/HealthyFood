namespace HealthyFood.Data.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using HealthyFood.Data.Common.Models;

    using static HealthyFood.Data.Common.DataValidation.CategoryValidation;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.BlogPosts = new HashSet<BlogPost>();
            this.Recipes = new HashSet<Recipe>();
        }

        [Required]
        [MaxLength(NameMaxLength)]
        public string Name { get; set; }

        [Required]
        [MaxLength(DescriptionMaxLength)]
        public string Description { get; set; }

        public virtual ICollection<BlogPost> BlogPosts { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }
    }
}

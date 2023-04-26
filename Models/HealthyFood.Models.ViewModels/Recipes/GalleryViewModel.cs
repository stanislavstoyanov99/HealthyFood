namespace HealthyFood.Models.ViewModels.Recipes
{
    using HealthyFood.Data.Models;
    using HealthyFood.Services.Mapping;

    public class GalleryViewModel : IMapFrom<Recipe>
    {
        public int Id { get; set; }

        public string ImagePath { get; set; }
    }
}

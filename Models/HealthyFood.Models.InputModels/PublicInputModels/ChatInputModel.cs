namespace HealthyFood.Models.InputModels.PublicInputModels
{
    using System.ComponentModel.DataAnnotations;

    public class ChatInputModel
    {
        [Display(Name = "Message")]
        public string Query { get; set; }
    }
}

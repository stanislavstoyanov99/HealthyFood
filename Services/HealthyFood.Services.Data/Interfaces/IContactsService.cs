namespace HealthyFood.Services.Data.Interfaces
{
    using System.Threading.Tasks;

    using HealthyFood.Models.ViewModels.Contacts;

    public interface IContactsService
    {
        Task SendContactToAdminAsync(ContactFormEntryViewModel contactFormEntryViewModel);
    }
}

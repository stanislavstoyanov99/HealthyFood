namespace HealthyFood.Web.Controllers
{
    using System.Threading.Tasks;

    using HealthyFood.Common.Attributes;
    using HealthyFood.Models.ViewModels.Contacts;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class ContactsController : Controller
    {
        private readonly IContactsService contactsService;

        public ContactsController(IContactsService contactsService)
        {
            this.contactsService = contactsService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactFormEntryViewModel contactFormViewModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(contactFormViewModel);
            }

            await this.contactsService.SendContactToAdminAsync(contactFormViewModel);

            return this.RedirectToAction("ThankYou");
        }

        [NoDirectAccess]
        public IActionResult ThankYou()
        {
            return this.View();
        }
    }
}

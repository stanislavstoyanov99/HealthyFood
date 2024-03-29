﻿namespace HealthyFood.Web.Controllers
{
    using System.Threading.Tasks;

    using HealthyFood.Models.ViewModels.Faq;
    using HealthyFood.Services.Data.Interfaces;

    using Microsoft.AspNetCore.Mvc;

    public class FaqController : Controller
    {
        private readonly IFaqService faqService;

        public FaqController(IFaqService faqService)
        {
            this.faqService = faqService;
        }

        public async Task<IActionResult> Index()
        {
            var faqs = await this.faqService.GetAllFaqsAsync<FaqDetailsViewModel>();

            return this.View(faqs);
        }
    }
}

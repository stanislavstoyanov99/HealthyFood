using Microsoft.AspNetCore.Mvc;

namespace HealthyFood.Web.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

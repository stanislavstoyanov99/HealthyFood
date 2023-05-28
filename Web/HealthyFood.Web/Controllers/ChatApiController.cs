namespace HealthyFood.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OpenAI_API;

    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ChatApiController : ControllerBase
    {
        private readonly OpenAIAPI openAiApi;

        public ChatApiController(OpenAIAPI openAiApi)
        {
            this.openAiApi = openAiApi;
        }

        [HttpGet]
        public async Task<IActionResult> GetResponse([FromQuery] string query)
        {
            var chat = this.openAiApi.Chat.CreateConversation();

            chat.AppendUserInput(query);

            var response = await chat.GetResponseFromChatbotAsync();

            return this.CreatedAtAction(nameof(this.GetResponse), response);
        }
    }
}

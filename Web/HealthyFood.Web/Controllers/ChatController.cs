namespace HealthyFood.Web.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using OpenAI_API;

    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly OpenAIAPI openAiApi;

        public ChatController(OpenAIAPI openAiApi)
        {
            this.openAiApi = openAiApi;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetResponse(string query, string username)
        {
            var chat = this.openAiApi.Chat.CreateConversation();

            if (!string.IsNullOrWhiteSpace(username))
            {
                chat.AppendUserInputWithName(username, query);
            }
            else
            {
                chat.AppendUserInput(query);
            }

            var response = await chat.GetResponseFromChatbotAsync();

            return this.CreatedAtAction(nameof(this.GetResponse), response);
        }
    }
}

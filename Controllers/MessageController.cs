using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using static TelegramBot.Startup;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramBot.Models;

namespace TelegramBot.Controllers
{
    public class MessageController : Controller
    {
        public async Task<OkResult> Update([FromBody] Update update)
        {
            if (update == null) return Ok();

            var client = await Bot.Get();
            var commands = Bot.Commands;

            if (update.Type == UpdateType.Message && update.Message.Text != null)
            {
                Message message = update.Message;
                if (message.Sticker != null) return Ok();

#if DEBUG
                int debugChat = Configuration.GetSection("TgSettings").GetValue<int>("DebugChatId");
                if (message.Chat.Id != debugChat)
                {
                    Task.Run(() => commands[0].Execute(message, client));
                    return Ok();
                }
#endif

                foreach (var command in commands)
                {
                    if (command.Contains(message.Text))
                    {
                        Task.Run(() => command.Execute(message, client));
                        break;
                    }
                }
            }

            return Ok();
        }
    }
}

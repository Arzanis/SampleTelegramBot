using Microsoft.AspNetCore.Mvc;
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

                if (AppSettings.DebugFlag && message.Chat.Id != AppSettings.DebugChatId)
                {
                    Task.Run(() => commands[0].Execute(message, client));
                    return Ok();
                }

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

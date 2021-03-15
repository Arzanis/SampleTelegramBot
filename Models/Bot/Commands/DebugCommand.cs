using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace TelegramBot.Models.Commands
{
    public class DebugCommand : Command
    {
        public override string Name => "debug";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            string answer = "Бот в режиме дебага, пожалуйста, подождите или пните разраба.";

            await client.SendTextMessageAsync(chatId, answer, replyToMessageId: messageId);
        }
    }
}
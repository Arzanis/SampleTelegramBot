using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;

namespace TelegramBot.Models.Commands
{
    public class Dice : Command
    {
        public override string Name => "dice";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            await client.SendDiceAsync(chatId, replyToMessageId: messageId);
        }
    }
}
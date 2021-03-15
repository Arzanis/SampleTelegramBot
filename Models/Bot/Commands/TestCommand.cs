using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading.Tasks;
using static System.String;

namespace TelegramBot.Models.Commands
{
    public class Test : Command
    {
        public override string Name => "test";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            long chatId = message.Chat.Id;
            int messageId = message.MessageId;
            int userId = message.From.Id;
            string messageDate = message.Date.ToString("MM/dd/yyyy HH:mm:ss");

            string answer = Format("User ID - {0}; Chat ID - {1}\nDatetime - {2}; Message - {3}", userId.ToString(), chatId.ToString(), messageDate, message.Text);

            await client.SendTextMessageAsync(chatId, answer, replyToMessageId: messageId);
        }
    }
}
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TelegramBot.Models.Commands
{
    public class FactCommand : Command
    {
        public override string Name => "fact";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;
            string url = "https://randstuff.ru/fact/generate/";
            string answer = "";

            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            Stream data = response.GetResponseStream();
            using (StreamReader sr = new StreamReader(data))
            {
                answer = sr.ReadToEnd();
            }

            await client.SendTextMessageAsync(chatId, answer, replyToMessageId: messageId);
        }
    }
}

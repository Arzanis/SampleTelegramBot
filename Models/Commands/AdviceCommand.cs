using Telegram.Bot;
using Telegram.Bot.Types;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TelegramBot.Models.Commands
{
    public class AdviseCommand : Command
    {
        public override string Name => "advise";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            string url = "http://fucking-great-advice.ru/api/random";

            WebClient WebClient = new WebClient { Encoding = Encoding.UTF8 };
            string response = await WebClient.DownloadStringTaskAsync(url);
            dynamic json = JsonConvert.DeserializeObject(response);
            string answer = json.text;

            await client.SendTextMessageAsync(chatId, answer, replyToMessageId: messageId);
        }
    }
}
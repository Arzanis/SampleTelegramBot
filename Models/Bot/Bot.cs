using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Models.Commands;
using Telegram.Bot.Types.Enums;
using Microsoft.Extensions.Configuration;
using static TelegramBot.Startup;

namespace TelegramBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;
        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();
        public static async Task<TelegramBotClient> Get()
        {
            string url = Configuration.GetSection("AppSettings").GetValue<string>("AppUrl");
            string hook = string.Format(url, "api/message/update");

            string apiKey = Configuration.GetSection("TgSettings").GetValue<string>("ApiKey");

            if (client != null)
            {
                return client;
            }

            commandsList = new List<Command>();
            commandsList.Add(new DebugCommand());
            commandsList.Add(new Test());
            commandsList.Add(new HelloCommand());
            commandsList.Add(new AdviseCommand());
            commandsList.Add(new Dice());
            commandsList.Add(new RedmineCommand());

            client = new TelegramBotClient(apiKey);

            await client.SetWebhookAsync(hook, allowedUpdates: new List<UpdateType> { UpdateType.Message, UpdateType.InlineQuery });

            return client;
        }
    }
}
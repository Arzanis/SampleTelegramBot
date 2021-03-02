using System.Collections.Generic;
using System.Threading.Tasks;
using Telegram.Bot;
using TelegramBot.Models.Commands;
using Telegram.Bot.Types.Enums;

namespace TelegramBot.Models
{
    public static class Bot
    {
        private static TelegramBotClient client;
        private static List<Command> commandsList;

        public static IReadOnlyList<Command> Commands => commandsList.AsReadOnly();

        public static async Task<TelegramBotClient> Get()
        {
            string hook = string.Format(AppSettings.Url, "api/message/update");

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

            client = new TelegramBotClient(AppSettings.Key);

            await client.SetWebhookAsync(hook, allowedUpdates: new List<UpdateType> { UpdateType.Message, UpdateType.InlineQuery });

            return client;
        }
    }
}
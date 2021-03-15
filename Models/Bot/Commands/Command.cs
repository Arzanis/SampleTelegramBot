using Telegram.Bot.Types;
using Telegram.Bot;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using static TelegramBot.Startup;

namespace TelegramBot.Models.Commands
{
    public abstract class Command
    {
        public abstract string Name { get; }
        private string BotName => Configuration.GetSection("TgSettings").GetValue<string>("Botname");

        public abstract Task Execute(Message message, TelegramBotClient client);

        public bool Contains(string command)
        {
            return command.Contains(Name) && command.Contains(BotName);
        }
    }
}
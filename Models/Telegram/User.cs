using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot.Models
{
    public class User
    {

        public int Id { get; set; }
        public bool IsBot { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string LanguageCode { get; set; }

        public User(int id, bool isBot, string firstName, string lastName, string username, string languageCode)
        {
            Id = id;
            IsBot = isBot;
            FirstName = firstName;
            LastName = lastName;
            Username = username;
            LanguageCode = languageCode;
        }

        public User GetUserFromTg(Telegram.Bot.Types.User tgUser)
        {
            return new User(tgUser.Id, tgUser.IsBot, tgUser.FirstName, tgUser.LastName, tgUser.Username, tgUser.LanguageCode);
        }
    }
}

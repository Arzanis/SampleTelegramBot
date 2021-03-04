using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Collections.Generic;
using System.Collections.Specialized;
using Redmine.Net.Api;
using Redmine.Net.Api.Types;
using static System.String;
using System;
using System.Threading.Tasks;

namespace TelegramBot.Models.Commands
{
    public class RedmineCommand : Command
    {
        private Dictionary<int, string> GetUserDict()
        {
            Dictionary<int, string> userDict = new Dictionary<int, string>
            {
                { 437852020, "6275" }, //Gladkih
                { 127019690, "6087" }, //Tihonin
                { 269068668, "6557" }, //Yasnikov
                { 138808788, "5729" } //Negashev
            };

            return userDict;
        }

        public override string Name => "rm";

        public override async Task Execute(Message message, TelegramBotClient client)
        {
            long chatId = message.Chat.Id;
            int messageId = message.MessageId;
            int userId = message.From.Id;
            string answer = Empty;

            try
            {
                string host = AppSettings.RmUrl;
                string apiKey = AppSettings.RmApiKey;

                Dictionary<int, string> userMap = GetUserDict();
                string rmUserId = userMap.ContainsKey(userId) ? userMap[userId] : Empty;

                if (rmUserId == Empty)
                    answer = "Неизвестный пользователь РМ";
                else
                {
                    var manager = new RedmineManager(host, apiKey);
                    NameValueCollection parameters = new NameValueCollection();

                    if (message.Text.Split(' ').Length > 1)
                    {
                        string taskId = message.Text.Split(' ')[1];
                        int intTaskId;

                        if (int.TryParse(taskId, out intTaskId))
                        {
                            parameters.Add(RedmineKeys.ISSUE_ID, taskId);
                            parameters.Add(RedmineKeys.STATUS_ID, RedmineKeys.ALL);

                            var issues = manager.GetObjects<Issue>(parameters);

                            if (issues.Count == 0)
                                answer = "Задача не найдена!";
                            else
                            {
                                foreach (var issue in issues)
                                {
                                    string formattedId = Format("<u><a href= \"{0}issues/{1}/\" >{1,6}</a></u>", host, issue.Id.ToString());
                                    string formattedDescr = issue.Description.Substring(0, issue.Description.Length > 4000 ? 4000 : issue.Description.Length);

                                    answer = Concat(answer, Format("{0}\n{1}", formattedId, formattedDescr));
                                }

                            }
                        }
                        else
                        {
                            answer = "Номер задачи некорректен!";
                        }
                    }
                    else
                    {
                        parameters.Add(RedmineKeys.ASSIGNED_TO_ID, rmUserId);
                        parameters.Add(RedmineKeys.STATUS, "o");

                        List<Issue> issues = manager.GetObjects<Issue>(parameters);


                        foreach (var issue in issues)
                        {
                            string formattedId = Format("<u><a href= \"{0}issues/{1}/\" >{1,6}</a></u>", host, issue.Id.ToString());
                            string formattedSubj = issue.Subject.Substring(0, issue.Subject.Length > 60 ? 60 : issue.Subject.Length);

                            answer = Concat(answer, Format("{0} - {1}\n", formattedId, formattedSubj));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                answer = Format("Кривые руки разраба выкинули эксепшн: {0}", ex.Message);
            }
            finally
            {
                await client.SendTextMessageAsync(chatId, answer, replyToMessageId: messageId, parseMode: ParseMode.Html);
            }
        }
    }
}
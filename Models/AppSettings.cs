namespace TelegramBot.Models
{
    public static class AppSettings
    {
        public static string AppUrl { get; set; } = "https://www.arzanis.me/{0}";

        public static string BotName { get; set; } = "FuckingAdviserBot";
        public static string TgApiKey { get; set; } = "1339776940:AAEDCw--99Y-GVxeGADagGj5bdZGWq9-FMY";
        public static bool DebugFlag { get; set; } = false;

        public static int DebugChatId { get; set; } = -590873509;

        public static string RmUrl { get; set; } = "https://redmine.permenergosbyt.ru/";

        public static string RmApiKey { get; set; } = "150c2364b49eea8cfb1cd64b81033fcf4bcf5551";
    }
}

namespace TelegramBot.Models
{
    public static class AppSettings
    {
        public static string Url { get; set; } = "https://fuckingadviserbot.azurewebsites.net:443/{0}";

        public static string Name { get; set; } = "FuckingAdviserBot";

        public static string Key { get; set; } = "1339776940:AAEDCw--99Y-GVxeGADagGj5bdZGWq9-FMY";

        public static bool DebugFlag { get; set; } = false;

        public static int DebugChat { get; set; } = -590873509;

        public static string RMUrl { get; set; } = "https://redmine.permenergosbyt.ru/";

        public static string RMApiKey { get; set; } = "150c2364b49eea8cfb1cd64b81033fcf4bcf5551";
    }
}

using Microsoft.AspNetCore.Mvc;
 
namespace TelegramBot.Controllers
{
    public class HomeController : Controller
    {
        public string Index()
        {
            return "Telegram bot @FuckingAdviser live here.";
        }
    }
}

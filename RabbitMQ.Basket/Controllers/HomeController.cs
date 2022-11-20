using Microsoft.AspNetCore.Mvc;

namespace RabbitMQ.Basket.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}

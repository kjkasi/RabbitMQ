using Microsoft.AspNetCore.Mvc;

namespace RabbitMQ.Catalog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return new RedirectResult("~/swagger");
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Furnituremarket.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

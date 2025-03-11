using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace MVCWebApp.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

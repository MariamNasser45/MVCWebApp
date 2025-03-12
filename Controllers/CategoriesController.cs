using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

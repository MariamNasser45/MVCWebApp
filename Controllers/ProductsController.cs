using Microsoft.AspNetCore.Mvc;

namespace ProductCatalog.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

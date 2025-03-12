using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Interfaces;
using ProductCatalog.ViewModels;

namespace ProductCatalog.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            CreateProductViewModel viewModel = new CreateProductViewModel()
            {
                Category = await _unitOfWork.CategoryServices.GetAllCategories()

            };

            return View(viewModel);
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProductCatalog.Interfaces;
using ProductCatalog.Models.Enums;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize("Admin")]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                CreateProductViewModel viewModel = new CreateProductViewModel()
                {
                    Category = await _unitOfWork.CategoryServices.GetAllCategories()
                };

                return View(viewModel);
            }
            else
            {
                var result = await _unitOfWork.ProductServices.CreateNewProduct(model);

                if (result==string.Empty)
                    return RedirectToAction(nameof(Index));
                else
                    return BadRequest(result);
            }
        }
    }
}

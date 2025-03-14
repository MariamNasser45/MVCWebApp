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

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            ViewBag.CategoryList = await _unitOfWork.CategoryServices.GetAllCategories();

            var allProducts = await _unitOfWork.ProductServices.GetAllProducts(null);

            return View(allProducts.ProductData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(int categoryId)
        {

            ViewBag.CategoryList = await _unitOfWork.CategoryServices.GetAllCategories();

            var allProducts = await _unitOfWork.ProductServices.GetAllProducts(categoryId);

            return View(allProducts.ProductData);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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

                if (result == string.Empty)
                    return RedirectToAction(nameof(Index));
                else
                    return BadRequest(result);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int productId)
        {
            var result = await _unitOfWork.ProductServices.GetProductById(productId);

            if (result.Messege == string.Empty && result.Product != null)
            {

                UpdateProductViewModel viewModel = new UpdateProductViewModel
                {
                    Name = result.Product.Name,
                    Price = result.Product.Price,
                    StartDate = result.Product.StartDate,
                    Duration = result.Product.Duration,
                    CategoryId = result.Product.CategoryId,
                    Category = await _unitOfWork.CategoryServices.GetAllCategories(),
                    Id = result.Product.Id

                };
                return View(viewModel);
            }
            else
            {
                //Use TempData to pass data from view to othe
                TempData["Error Messege"] = result.Messege;

                return RedirectToAction("Error", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(UpdateProductViewModel viewModel)
        {
            if(!ModelState.IsValid)
            {
                var categories = await _unitOfWork.CategoryServices.GetAllCategories();

                viewModel.Category = categories;

                return View(viewModel);
            }
            else
            {
                var result = await _unitOfWork.ProductServices.UpdateProduct(viewModel);

                if (result.Messege == string.Empty)
                    return RedirectToAction(nameof(Index));
                else
                {
                    TempData["Error Messege"] = result.Messege;


                    return RedirectToAction("Error", "Home");
                }
                    
            }
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Details(int productId)
        {
            var product = await _unitOfWork.ProductServices.GetProductById(productId);

            if(product.Messege==string.Empty && product.Product!=null)
            {
                return View(product.Product);
            }
            else
            {
                TempData["Error Messege"] = product.Messege;


                return RedirectToAction("Error", "Home");
            }

        }

        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _unitOfWork.ProductServices.DeleteProduct(id);

            if (result == string.Empty)
                return Ok("Product Deleted Succesfully");
            else
            {
                TempData["Error Messege"] = result;


                return RedirectToAction("Error", "Home");
            }
        }
    }
}

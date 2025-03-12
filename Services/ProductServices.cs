using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Interfaces;
using ProductCatalog.Models;

namespace ProductCatalog.Services
{
    public class ProductServices : BaseRepository<Product> , IProductServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public ProductServices(ApplicationDbContext context, IUnitOfWork unitOfWork):base(context)
        {
            _context=context;
            _unitOfWork=unitOfWork;
        }

        public async Task CreateNewProduct(CreateProductViewModel viewModel)
        {

        }

        public async Task<List<Product>> GetAllProducts()
        {
            var products = await GetAllWithData();

            return products.ToList();
        }

        public async Task<Product> GetProductById(int id)
        {
            var output = new Product();

            var getProduct = await FindByIdWithData(id);

            return output;
        }

        public async Task<string> DeleteProduct(int id)
        {
            var getProduct = await FindByIdWithData(id);

            if (getProduct != null)
            {
                var result = await Delete(getProduct);

                if (result == string.Empty)
                {
                    return string.Empty;
                }
                else
                {
                    return result;
                }
            }
            else
                return "Invalid Product";

        }

        public async Task<Product> UpdateProduct(int UpdateProductVieModel)
        {
            var output = new Product();

            return output;
        }
    }
}

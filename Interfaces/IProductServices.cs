using ProductCatalog.Models;

namespace ProductCatalog.Interfaces
{
    public interface IProductServices : IBaseRepository<Product>
    {
        Task<string> CreateNewProduct(CreateProductViewModel model);
        Task<ReturnListOfProductViewModel> GetAllProducts(int? categoryId);
        Task<ReturnaProductDataViewModel> GetProductById(int id);
        Task<string> DeleteProduct(int id);
        Task<ReturnaProductDataViewModel> UpdateProduct(UpdateProductVieModel model);

    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using ProductCatalog.Models;

namespace ProductCatalog.Interfaces
{
    public interface ICategoryServices : IBaseRepository<Category>
    {
        Task<List<SelectListItem>> GetAllCategories();
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductCatalog.Data;
using ProductCatalog.Interfaces;
using ProductCatalog.Models;

namespace ProductCatalog.Services
{
    public class CategoryServices : BaseRepository<Category> , ICategoryServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryServices(ApplicationDbContext context, IUnitOfWork unitOfWork) : base(context)
        {
            _context=context;
            _unitOfWork=unitOfWork;
        }

        public async Task<List<SelectListItem>> GetAllCategories()
        {
            var categories = await _context.Categories.AsNoTracking().Select(i => new SelectListItem { Value = i.Id.ToString(), Text = i.Name }).ToListAsync();

            if(categories.Count!=0)
                return categories;
            return new List<SelectListItem>();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.SingleOrDefaultAsync(i => i.Id==id);
        }
    }

}

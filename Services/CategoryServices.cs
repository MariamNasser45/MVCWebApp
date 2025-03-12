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
    }

}

using MVCWebApp.Data;
using MVCWebApp.Interfaces;
using MVCWebApp.Models;

namespace MVCWebApp.Services
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

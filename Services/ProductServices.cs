using MVCWebApp.Data;
using MVCWebApp.Interfaces;
using MVCWebApp.Models;

namespace MVCWebApp.Services
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
    }
}

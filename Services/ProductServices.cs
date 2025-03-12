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
    }
}

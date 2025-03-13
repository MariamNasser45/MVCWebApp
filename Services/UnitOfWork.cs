using ProductCatalog.Data;
using ProductCatalog.Interfaces;

namespace ProductCatalog.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IProductServices ProductServices { get; set; }
        public ICategoryServices CategoryServices { get; set; }
        public IUserResolverService UserResolverService { get; set; }
        public UnitOfWork(ApplicationDbContext context , IUserResolverService userResolver)
        {
            _context=context;
            ProductServices = new ProductServices(_context,this);
            CategoryServices = new CategoryServices(_context,this);
            UserResolverService = userResolver;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}

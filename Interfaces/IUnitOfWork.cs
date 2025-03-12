using ProductCatalog.Data;

namespace ProductCatalog.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductServices ProductServices { get; }
        ICategoryServices CategoryServices { get; }
    }
}

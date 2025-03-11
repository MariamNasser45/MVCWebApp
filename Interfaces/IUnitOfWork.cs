using MVCWebApp.Data;

namespace MVCWebApp.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IProductServices ProductServices { get; }
        ICategoryServices CategoryServices { get; }
    }
}

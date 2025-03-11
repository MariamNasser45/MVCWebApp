using MVCWebApp.Data;
using MVCWebApp.Interfaces;

namespace MVCWebApp.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context=context;
        }
    }
}

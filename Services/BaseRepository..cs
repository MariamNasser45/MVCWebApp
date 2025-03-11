using Microsoft.EntityFrameworkCore;
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

        public async Task<T> Add(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return entity;
        }

        public async Task<T> Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
            return entity;
        }

        public async Task<T> FindById(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity==null)
                return null;
            else
                _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            return entity;
        }

        public async Task<T> FindByIdWithData(int id)
        {
            var collection = await GetCollection(typeof(T));

            var entry  = await FindById(id);

            if (entry==null)
                return null;
            else
            {
                IQueryable<T> queryable =  _context.Set<T>().AsNoTracking();

                foreach(var col in collection )
                {
                    queryable = queryable.Include(col).AsNoTracking();
                }

                var entity =await queryable.Where(i => i.Equals(entry)).AsNoTracking().SingleOrDefaultAsync();

                _context.Entry(entity).State = EntityState.Detached;

                return entity;
            }

        }

        public async Task<List<string>> GetCollection(Type type)
        {
            var collection = type.GetProperties()
                .Where(p=>(typeof(T).IsAssignableFrom(p.PropertyType)
                && p.PropertyType!=typeof(string)
                && p.PropertyType != typeof(byte[])
                && p.PropertyType.Namespace==type.Namespace)).Select(n=>n.Name).ToList();

            return collection;
        }
        public async Task<IEnumerable<T>> GetAll()
        {
            var entities = await _context.Set<T>().ToListAsync();

            if (entities.Count()==0)
                return null;

            return entities;

        }

        public async Task<IEnumerable<T>> GetAllWithData()
        {
            var entries = _context.Set<T>().AsNoTracking().AsQueryable();

            var includes = await GetCollection(typeof(T));

            foreach(var inc in includes)
            {
                entries.Include(inc);
            }

            return await entries.ToListAsync();

        }
        public async Task<T> Update(T entity)
        {
             _context.Set<T>().Update(entity);

            return entity;

        }

    }
}

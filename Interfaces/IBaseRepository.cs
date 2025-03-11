namespace MVCWebApp.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> GetAllWithData();
        Task<T> FindById(int id);
        Task<T> FindByIdWithData(int id);
    }
}

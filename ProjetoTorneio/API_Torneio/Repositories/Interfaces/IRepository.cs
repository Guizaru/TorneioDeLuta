namespace API_Torneio.Repositories.Interfaces
{
    public interface IRepository<T>
    {
        Task<List<T>> GetAll();
        Task<T?> Get(int id);
        Task Add (T entity);
        Task Update (T entity);
        Task Delete (T entity);
    }
}

namespace Campus.Repository.Helpers;

public interface IGenericRepository<T> where T : class
{
    Task<T> GetByIdAsync(object id);
    T GetById(object id);
    Task<IEnumerable<T>> GetAllAsync();
    Task InsertAsync(T entity);
    void Delete(object id);
    Task Update(T entity, object id);
}

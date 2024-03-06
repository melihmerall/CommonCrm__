namespace CommonCrm.Data.Repositories.Abstract;

public interface IRepository<T>
{
    Task<T> GetById(int id);

    Task<List<T>> GetAll();

    Task Create(T entity);

    Task Update(T entity);
    Task Delete(T entity);
}
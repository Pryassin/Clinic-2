namespace DataLayer.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        int Add(T entity);
        bool Delete(T entity);
        T GetByID(int id);
        bool Update(T entity);

        bool DoesExist(int id);
    }
}
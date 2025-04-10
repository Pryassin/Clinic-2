namespace DataLayer.Repositories.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        IEnumerable<Person> GetAll();
        IEnumerable<Person> GetByName(string name);
    }
}

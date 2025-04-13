namespace DataLayer.Repositories.Interfaces
{
    public interface IPersonRepository : IBaseRepository<Person>
    {
        IQueryable<Person> GetAll();
        IQueryable<Person> GetByName(string name);
        Person GetByEmail(string email);
        Person GetByPhone(string phone);

    }
}

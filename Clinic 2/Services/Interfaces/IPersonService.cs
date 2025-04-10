
public interface IPersonService
{
 
    IEnumerable<Person> GetAll();
    IEnumerable<Person> GetByName(string name);
}
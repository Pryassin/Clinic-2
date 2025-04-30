
using DataLayer.Repositories.Interfaces;

public interface IPersonService
{
  int AddPerson(Person person);
    bool DeletePerson(int id);
    Person GetPersonById(int id);
    bool UpdatePerson(Person person);


}
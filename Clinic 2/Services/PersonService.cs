
using System.Reflection.Metadata.Ecma335;
using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }
    void EnsurePersonIsNotNull(Person person)
    {
        if (person == null)
            throw new ArgumentNullException(nameof(person), "Person cannot be null");
    }
    public int AddPerson(Person person)
    {
        EnsurePersonIsNotNull(person);
        var result = _personRepository.Add(person);
        if (result <= 0)
        {
            throw new Exception("Person failed to save to the database.");
        }
        return result;
    }

    public bool DeletePerson(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
        }
        var person = _personRepository.GetByID(id);
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with ID {id} not found");
        }
       if( _personRepository.Delete(person)==false)
        {
            throw new Exception("Person failed to delete from the database.");
        }
        return true;
    }

    public Person GetPersonById(int id)
    {
        if (id < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
        }
        var person = _personRepository.GetByID(id);
        if (person == null)
        {
            throw new KeyNotFoundException($"Person with ID {id} not found");
        }
        return person;
    }

    public bool UpdatePerson(Person person)
    {
        EnsurePersonIsNotNull(person);
        var existingPerson = _personRepository.GetByID(person.PersonID);
        if (existingPerson == null)
        {
            throw new KeyNotFoundException($"Person with ID {person.PersonID} not found");
        }
        if (existingPerson.PersonID != person.PersonID)
        {
            throw new InvalidOperationException("Cannot update a person with a different ID");
        }
        if (_personRepository.Update(person) == true)
        {
            return true;
        }
        else
        {
            throw new Exception("Failed to update person");
        }
    }
}

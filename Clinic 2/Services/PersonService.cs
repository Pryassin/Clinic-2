
using System.Reflection.Metadata.Ecma335;
using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

/// <summary>
/// Provides services for managing Person entities, including add, update, delete, and retrieval operations.
/// </summary>
public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="PersonService"/> class.
    /// </summary>
    /// <param name="personRepository">The repository used for person data access.</param>
    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    /// <summary>
    /// Ensures that the provided person object is not null.
    /// </summary>
    /// <param name="person">The person object to check.</param>
    /// <exception cref="ArgumentNullException">Thrown if the person is null.</exception>
    private void EnsurePersonIsNotNull(Person person)
    {
        if (person == null)
            throw new ArgumentNullException(nameof(person), "Person cannot be null");
    }

    /// <summary>
    /// Adds a new person to the repository.
    /// </summary>
    /// <param name="person">The person to add.</param>
    /// <returns>The ID of the newly added person.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the person is null.</exception>
    /// <exception cref="Exception">Thrown if the person could not be saved.</exception>
    public int AddPerson(Person person)
    {
        EnsurePersonIsNotNull(person);
        var result = _personRepository.Add(person);
        if (result <= 0)
        {
            throw new Exception("Person failed to save to the database.");
        }
        return person.PersonId;
    }

    /// <summary>
    /// Deletes a person by their ID.
    /// </summary>
    /// <param name="id">The ID of the person to delete.</param>
    /// <returns>True if the person was deleted successfully.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the ID is less than zero.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the person is not found.</exception>
    /// <exception cref="Exception">Thrown if the person could not be deleted.</exception>
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

        if (!_personRepository.Delete(person))
        {
            throw new InvalidOperationException("Failed to delete person from database");
        }
        return true;
    }

    /// <summary>
    /// Retrieves a person by their ID.
    /// </summary>
    /// <param name="id">The ID of the person to retrieve.</param>
    /// <returns>The person with the specified ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the ID is less than zero.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the person is not found.</exception>
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

    /// <summary>
    /// Updates an existing person in the repository.
    /// </summary>
    /// <param name="person">The person object with updated information.</param>
    /// <returns>True if the update was successful.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the person is null.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the person does not exist.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the person ID does not match the existing record.</exception>
    /// <exception cref="Exception">Thrown if the update fails.</exception>
    public bool UpdatePerson(Person person)
    {
        EnsurePersonIsNotNull(person);
        var existingPerson = _personRepository.GetByID(person.PersonId);
        if (existingPerson == null)
        {
            throw new KeyNotFoundException($"Person with ID {person.PersonId} not found");
        }
        existingPerson.PersonId = person.PersonId;
        existingPerson.Name = person.Name;
        existingPerson.Address = person.Address;
        existingPerson.PhoneNumber = person.PhoneNumber;
        existingPerson.Email = person.Email;
        existingPerson.DateOfBirth = person.DateOfBirth;
        existingPerson.Gender = person.Gender;
        return _personRepository.Update(existingPerson);


    }
}

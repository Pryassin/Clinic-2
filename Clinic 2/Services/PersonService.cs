
using System.Reflection.Metadata.Ecma335;
using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PersonService : PersonRepository, IPersonService
{

    private ClinicDbContext _context;
    public PersonService(ClinicDbContext context):base(context)
    {
        _context = context;
    }

    public IEnumerable<Person> GetAll()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Person> GetByName(string name)
    {
        throw new NotImplementedException();
    }
}

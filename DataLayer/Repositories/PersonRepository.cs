using DataLayer.Data;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class PersonRepository : BaseRepository<Person>, IPersonRepository
    {
        private readonly ClinicDbContext _context;
        public PersonRepository(ClinicDbContext context) : base(context)
        {
            _context = context;
        }
        IEnumerable<Person> IPersonRepository.GetAll()
        {
            return _context.Set<Person>().ToList(); // Corrected the syntax and added ToList() to fetch all records
        }

        IEnumerable<Person> IPersonRepository.GetByName(string name)
        {
           return _context.Set<Person>()
                .Where(p => p.Name.Contains(name))
                .ToList(); 
            // Corrected the syntax and added ToList() to fetch records by name
        }
    }
}
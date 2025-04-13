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
        public IQueryable<Person> GetAll()
        {
            return _context.Set<Person>(); 
        }

        public Person GetByEmail(string email)
        {
            return _context.Set<Person>()
                .FirstOrDefault(p => p.Email == email);
        }

        public IQueryable<Person> GetByName(string name)
        {
            return _context.Set<Person>()
                 .Where(p => p.Name.Contains(name))
               ; 
            // Corrected the syntax and added ToList() to fetch records by name
        }

        public Person GetByPhone(string phone)
        {
            return _context.Set<Person>()
                .FirstOrDefault(p => p.PhoneNumber == phone);
        }
    }
}
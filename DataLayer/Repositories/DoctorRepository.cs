using DataLayer.Data;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {    private readonly ClinicDbContext _context;
        public DoctorRepository(ClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Doctor> GetBySpecialization(string spec)
        {
            return _context.Set<Doctor>().Where(Doctor => Doctor.Specialization == spec);
        }
    }
}

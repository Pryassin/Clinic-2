using DataLayer.Data;
using DataLayer.Repositories.Interfaces;

namespace DataLayer.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {
        public DoctorRepository(ClinicDbContext context) : base(context)
        {

        }
    }
}

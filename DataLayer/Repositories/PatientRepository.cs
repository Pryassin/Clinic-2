using DataLayer.Data;
using DataLayer.Repositories.Interfaces;

namespace DataLayer.Repositories
{
    public class PatientRepository : BaseRepository<Patient>, IPatientRepository
    {
        public PatientRepository(ClinicDbContext context) : base(context)
        {

        }
    }
}

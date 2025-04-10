using DataLayer.Data;
using DataLayer.Repositories.Interfaces;

namespace DataLayer.Repositories
{
    public class MedicalRecordRepository : BaseRepository<MedicalRecord>, IMedicalRecordRepository
    {
        public MedicalRecordRepository(ClinicDbContext context) : base(context)
        {

        }
    }
}

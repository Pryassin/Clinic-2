using DataLayer.Data;
using DataLayer.Repositories.Interfaces;

namespace DataLayer.Repositories
{
    public class PrescriptionRepository : BaseRepository<Prescription>, IPrescriptionsRepository
    {
        public PrescriptionRepository(ClinicDbContext context) : base(context)
        {
        }
    }
}

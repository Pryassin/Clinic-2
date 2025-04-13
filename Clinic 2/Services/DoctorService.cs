using Clinic_2.Services.Interfaces;
using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

namespace Clinic_2.Services
{
    public class DoctorService : DoctorRepository, IDoctorService
    {
        public DoctorService(ClinicDbContext context) : base(context)
        {
        }

       
    }
}


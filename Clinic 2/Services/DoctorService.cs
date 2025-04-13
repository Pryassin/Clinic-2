using Clinic_2.Services.Interfaces;
using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

namespace Clinic_2.Services
{
    public class DoctorService : IDoctorService
    {
        IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

       
    }
}


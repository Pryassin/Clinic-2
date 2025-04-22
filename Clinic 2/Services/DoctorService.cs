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
        private void EnsureDoctorNotNull(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor), "Doctor cannot be null");
        }
        public bool AddDoctor(Doctor doctor)
        {
            EnsureDoctorNotNull(doctor);
            if (_doctorRepository.DoesExist(doctor.DoctorID))
            {
                throw new InvalidOperationException("Doctor already exists.");
            }
      
            return _doctorRepository.Add(doctor) > 0;
        }

        public bool DeleteDoctor(Doctor doctor)
        {
            EnsureDoctorNotNull(doctor);
            if (!_doctorRepository.DoesExist(doctor.DoctorID))
            {
                throw new InvalidOperationException("Doctor does not exist.");
            }
            return _doctorRepository.Delete(doctor);

        }

        public Doctor FindDoctorById(int id)
        {
            if(id < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero");
            }
        
            var doctor = _doctorRepository.GetByID(id);
            if (doctor == null)
            {
                throw new InvalidOperationException("Doctor not found.");
            }
            return doctor;
        }

        public Doctor FindDoctorByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name), "Name cannot be null or empty");
            }

            var doctor = _doctorRepository.GetDoctorByName(name);
            if (doctor == null)
            {
                throw new InvalidOperationException("Doctor not found.");
            }
            return doctor;
        }

        public bool UpdateDoctor(Doctor doctor)
        {
            EnsureDoctorNotNull(doctor);
            if (!_doctorRepository.DoesExist(doctor.DoctorID))
            {
                throw new InvalidOperationException("Doctor does not exist.");
            }
            return _doctorRepository.Update(doctor);
        }

        public IQueryable<Doctor> GetDoctorsBySpecialization(string spec)
        {
            if(string.IsNullOrWhiteSpace(spec))
            {
                throw new ArgumentNullException(nameof(spec), "Specialization cannot be null or empty");
            }
            var doctors = _doctorRepository.GetBySpecialization(spec);
            if (doctors == null || !doctors.Any())
            {
                throw new InvalidOperationException("No doctors found with the specified specialization.");
            }
            return doctors;
        }

        public IQueryable<Doctor> GetDoctorsWithAppointmentsToday()
        {
           var doctors= _doctorRepository.GetDoctorsWithAppointmentsToday();
            if(doctors == null || !doctors.Any())
            {
                throw new InvalidOperationException("No doctors found with appointments today.");
            }
            return doctors;
        }
    }
}


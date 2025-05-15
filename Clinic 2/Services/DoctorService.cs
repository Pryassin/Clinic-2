using Clinic_2.Services.Interfaces;
using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

namespace Clinic_2.Services
{
    /// <summary>
    /// Service class for managing doctor-related operations.
    /// </summary>
    public class DoctorService : IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoctorService"/> class.
        /// </summary>
        /// <param name="doctorRepository">The doctor repository dependency.</param>
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        /// <summary>
        /// Ensures the provided doctor object is not null.
        /// </summary>
        /// <param name="doctor">The doctor object to check.</param>
        /// <exception cref="ArgumentNullException">Thrown if doctor is null.</exception>
        private void EnsureDoctorNotNull(Doctor doctor)
        {
            if (doctor == null)
                throw new ArgumentNullException(nameof(doctor), "Doctor cannot be null");
        }

        /// <summary>
        /// Adds a new doctor to the repository.
        /// </summary>
        /// <param name="doctor">The doctor to add.</param>
        /// <returns>The ID of the added doctor.</returns>
        /// <exception cref="ArgumentNullException">Thrown if doctor is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if doctor already exists.</exception>
        public int AddDoctor(Doctor doctor)
        {
            EnsureDoctorNotNull(doctor);
            if (_doctorRepository.DoesExist(doctor.DoctorID))
            {
                throw new InvalidOperationException("Doctor already exists.");
            }
            return _doctorRepository.Add(doctor);
        }

        /// <summary>
        /// Deletes a doctor from the repository.
        /// </summary>
        /// <param name="doctor">The doctor to delete.</param>
        /// <returns>True if deleted; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if doctor is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if doctor does not exist.</exception>
        public bool DeleteDoctor(Doctor doctor)
        {
            EnsureDoctorNotNull(doctor);
            if (!_doctorRepository.DoesExist(doctor.DoctorID))
            {
                throw new InvalidOperationException("Doctor does not exist.");
            }
            return _doctorRepository.Delete(doctor);
        }

        /// <summary>
        /// Finds a doctor by their ID.
        /// </summary>
        /// <param name="id">The doctor's ID.</param>
        /// <returns>The found doctor.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if ID is less than zero.</exception>
        /// <exception cref="InvalidOperationException">Thrown if doctor is not found.</exception>
        public Doctor FindDoctorById(int id)
        {
            if (id < 0)
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

        /// <summary>
        /// Finds a doctor by their name.
        /// </summary>
        /// <param name="name">The doctor's name.</param>
        /// <returns>The found doctor.</returns>
        /// <exception cref="ArgumentNullException">Thrown if name is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if doctor is not found.</exception>
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

        /// <summary>
        /// Updates an existing doctor in the repository.
        /// </summary>
        /// <param name="doctor">The doctor to update.</param>
        /// <returns>True if updated; otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Thrown if doctor is null.</exception>
        /// <exception cref="InvalidOperationException">Thrown if doctor does not exist.</exception>
        public bool UpdateDoctor(Doctor doctor)
        {
            EnsureDoctorNotNull(doctor);
            if (!_doctorRepository.DoesExist(doctor.DoctorID))
            {
                throw new InvalidOperationException("Doctor does not exist.");
            }
            return _doctorRepository.Update(doctor);
        }

        /// <summary>
        /// Gets doctors by their specialization.
        /// </summary>
        /// <param name="spec">The specialization to filter by.</param>
        /// <returns>A queryable collection of doctors.</returns>
        /// <exception cref="ArgumentNullException">Thrown if specialization is null or empty.</exception>
        /// <exception cref="InvalidOperationException">Thrown if no doctors are found.</exception>
        public IQueryable<Doctor> GetDoctorsBySpecialization(string spec)
        {
            if (string.IsNullOrWhiteSpace(spec))
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

        /// <summary>
        /// Gets doctors who have appointments today.
        /// </summary>
        /// <returns>A queryable collection of doctors with appointments today.</returns>
        /// <exception cref="InvalidOperationException">Thrown if no doctors are found.</exception>
        public IQueryable<Doctor> GetDoctorsWithAppointmentsToday()
        {
            var doctors = _doctorRepository.GetDoctorsWithAppointmentsToday();
            if (doctors == null || !doctors.Any())
            {
                throw new InvalidOperationException("No doctors found with appointments today.");
            }
            return doctors;
        }
    }
}


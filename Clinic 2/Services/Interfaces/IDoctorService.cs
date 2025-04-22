namespace Clinic_2.Services.Interfaces
{
    public interface IDoctorService
    {
        bool AddDoctor(Doctor doctor);
        bool UpdateDoctor(Doctor doctor);
        bool DeleteDoctor(Doctor doctor);
        Doctor FindDoctorById(int id);
        Doctor FindDoctorByName(string name);
        public IQueryable<Doctor> GetDoctorsWithAppointmentsToday();
        public IQueryable<Doctor> GetDoctorsBySpecialization(string spec);

    }
}


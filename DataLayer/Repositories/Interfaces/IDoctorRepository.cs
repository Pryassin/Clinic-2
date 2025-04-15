namespace DataLayer.Repositories.Interfaces
{
    public interface IDoctorRepository : IBaseRepository<Doctor>
    {
        IQueryable<Doctor> GetBySpecialization(string spec);
        IQueryable<Doctor> GetDoctorsWithAppointmentsToday();
        Doctor? GetDoctorByName(string name);
    }
}

using DataLayer.Data;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DataLayer.Repositories
{
    public class DoctorRepository : BaseRepository<Doctor>, IDoctorRepository
    {    private readonly ClinicDbContext _context;
        public DoctorRepository(ClinicDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Doctor> GetBySpecialization(string spec)
        {
            return _context.Set<Doctor>().Where(Doctor => Doctor.Specialization == spec);
        }

        public Doctor ? GetDoctorByName(string name)
        {
            return _context.Doctors.FromSqlInterpolated($@" SELECT Doc.* FROM Doctors AS Doc  INNER JOIN Persons Per 
            ON Per.PersonID = Doc.PersonID  WHERE Per.Name = {name}").FirstOrDefault();
        }

        public IQueryable<Doctor> GetDoctorsWithAppointmentsToday()
        {
            return _context.Doctors
     .FromSqlRaw(" SELECT distinct dc.* FROM Doctors as dc inner join Appointments App ON App.DoctorID = dc.DoctorID" +
     " AND DAY(App.AppointmentDateTime) = DAY(GETDATE());");
        }
    }
}

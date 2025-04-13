using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class AppointmentRepository : BaseRepository<Appointments>, IAppointmentRepository
{
    private readonly ClinicDbContext _context;
    public AppointmentRepository(ClinicDbContext context) : base(context)
    {
        _context = context;
    }

    public bool CancelAppointment(Appointments appointment)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Appointments> GetAppointmentsByDoctorId(int doctorId)
    {
        throw new NotImplementedException();
    }

    public IQueryable<Appointments> GetAppointmentsByPatientId(int patientId)
    {
        throw new NotImplementedException();
    }

    public bool IsDoctorAvailable(int doctorId, DateTime dateTime)
    {
        return !_context.Appointments.Any(a => a.DoctorID == doctorId && a.AppointmentDateTime == dateTime &&
       ( a.AppointmentStatus == AppointmentStatus.Scheduled || a.AppointmentStatus == AppointmentStatus.Rescheduled));
    }

    public bool IsPatientAvailable(int patientId, DateTime dateTime)
    {
        return !_context.Appointments.Any(a => a.PatientID == patientId && a.AppointmentDateTime == dateTime &&
        (a.AppointmentStatus == AppointmentStatus.Scheduled || a.AppointmentStatus == AppointmentStatus.Rescheduled));
    }
    public bool IsPatientAvailableForRescheduel(int PatientID,DateTime dateTime,int AppointmentID)
    {
        return !_context.Appointments.Any(a => a.PatientID == PatientID && a.AppointmentDateTime == dateTime &&
        (a.AppointmentStatus == AppointmentStatus.Scheduled || a.AppointmentStatus == AppointmentStatus.Rescheduled) && a.AppointmentID != AppointmentID);
    }
    public bool IsDoctorAvailableForRescheduel(int DoctorID, DateTime dateTime,int AppointmentID)
    {
        return !_context.Appointments.Any(a => a.DoctorID == DoctorID && a.AppointmentDateTime == dateTime &&
        (a.AppointmentStatus == AppointmentStatus.Scheduled || a.AppointmentStatus == AppointmentStatus.Rescheduled) && a.AppointmentID!=AppointmentID);
    }
   
}
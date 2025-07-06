using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

/// <summary>
/// Repository for managing appointment data and availability logic.
/// </summary>
public class AppointmentRepository : BaseRepository<Appointments>, IAppointmentRepository
{
    private readonly ClinicDbContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppointmentRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public AppointmentRepository(ClinicDbContext context) : base(context)
    {
        _context = context;
    }

    /// <summary>
    /// Cancels the specified appointment by updating its status.
    /// </summary>
    /// <param name="appointment">The appointment to cancel.</param>
    /// <returns>True if the operation succeeded; otherwise, false.</returns>
    public bool CancelAppointment(Appointments appointment)
    {
        appointment.AppointmentStatus = enAppointmentStatus.Cancelled;
        _context.Appointments.Update(appointment);
        return _context.SaveChanges() > 0;
    }

    /// <summary>
    /// Gets all appointments for a specific doctor.
    /// </summary>
    /// <param name="doctorId">The doctor's ID.</param>
    /// <returns>An <see cref="IQueryable{Appointments}"/> of appointments.</returns>
    public IQueryable<Appointments> GetAppointmentsByDoctorId(int doctorId)
    {
        return _context.Appointments.Where(a => a.DoctorID == doctorId);
    }

    /// <summary>
    /// Gets all appointments for a specific patient.
    /// </summary>
    /// <param name="patientId">The patient's ID.</param>
    /// <returns>An <see cref="IQueryable{Appointments}"/> of appointments.</returns>
    public IQueryable<Appointments> GetAppointmentsByPatientId(int patientId)
    {
        return _context.Appointments.Where(a => a.PatientID == patientId);
    }

    /// <summary>
    /// Checks if a doctor is available at a specific date and time.
    /// </summary>
    /// <param name="doctorId">The doctor's ID.</param>
    /// <param name="dateTime">The date and time to check.</param>
    /// <returns>True if available; otherwise, false.</returns>
    public bool IsDoctorAvailable(int doctorId, DateTime dateTime)
    {
        return !_context.Appointments.Any(a => a.DoctorID == doctorId && a.AppointmentDateTime == dateTime &&
            (a.AppointmentStatus == enAppointmentStatus.Scheduled || a.AppointmentStatus == enAppointmentStatus.Rescheduled));
    }

    /// <summary>
    /// Checks if a patient is available at a specific date and time.
    /// </summary>
    /// <param name="patientId">The patient's ID.</param>
    /// <param name="dateTime">The date and time to check.</param>
    /// <returns>True if available; otherwise, false.</returns>
    public bool IsPatientAvailable(int patientId, DateTime dateTime)
    {
        return !_context.Appointments.Any(a => a.PatientID == patientId && a.AppointmentDateTime == dateTime &&
            (a.AppointmentStatus == enAppointmentStatus.Scheduled || a.AppointmentStatus == enAppointmentStatus.Rescheduled));
    }

    /// <summary>
    /// Checks if a patient is available for rescheduling, excluding a specific appointment.
    /// </summary>
    /// <param name="PatientID">The patient's ID.</param>
    /// <param name="dateTime">The new date and time.</param>
    /// <param name="AppointmentID">The appointment to exclude from the check.</param>
    /// <returns>True if available; otherwise, false.</returns>
    public bool IsPatientAvailableForRescheduel(int PatientID, DateTime dateTime, int AppointmentID)
    {
        return !_context.Appointments.Any(a => a.PatientID == PatientID && a.AppointmentDateTime == dateTime &&
            (a.AppointmentStatus == enAppointmentStatus.Scheduled || a.AppointmentStatus == enAppointmentStatus.Rescheduled) && a.AppointmentId != AppointmentID);
    }

    /// <summary>
    /// Checks if a doctor is available for rescheduling, excluding a specific appointment.
    /// </summary>
    /// <param name="DoctorID">The doctor's ID.</param>
    /// <param name="dateTime">The new date and time.</param>
    /// <param name="AppointmentID">The appointment to exclude from the check.</param>
    /// <returns>True if available; otherwise, false.</returns>
    public bool IsDoctorAvailableForRescheduel(int DoctorID, DateTime dateTime, int AppointmentID)
    {
        return !_context.Appointments.Any(a => a.DoctorID == DoctorID && a.AppointmentDateTime == dateTime &&
            (a.AppointmentStatus == enAppointmentStatus.Scheduled || a.AppointmentStatus == enAppointmentStatus.Rescheduled)
            && a.AppointmentId != AppointmentID);
    }
}

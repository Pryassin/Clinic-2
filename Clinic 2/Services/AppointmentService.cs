using DataLayer.Repositories.Interfaces;
/// <summary>
/// Provides services for managing appointments, including scheduling, rescheduling, canceling, and retrieving appointments.
/// </summary>
public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    /// <summary>
    /// Initializes a new instance of the <see cref="AppointmentService"/> class.
    /// </summary>
    /// <param name="appointmentRepo">The appointment repository dependency.</param>
    public AppointmentService(IAppointmentRepository appointmentRepo)
    {
        _appointmentRepository = appointmentRepo;
    }

    /// <summary>
    /// Reschedules an existing appointment to a new date and time.
    /// </summary>
    /// <param name="appointment">The appointment to reschedule.</param>
    /// <param name="newDateTime">The new date and time for the appointment.</param>
    /// <returns>True if the appointment was successfully rescheduled; otherwise, false.</returns>
    /// <exception cref="ArgumentException">Thrown if the new date is not in the future.</exception>
    public bool RescheduleAppointment(Appointments appointment, DateTime newDateTime)
    {
        if (newDateTime <= DateTime.Now)
        {
            throw new ArgumentException("Rescheduled date must be in the future.");
        }
        bool doctorAvailable = _appointmentRepository.IsDoctorAvailableForRescheduel(
            appointment.DoctorID, newDateTime, appointment.AppointmentID);

        bool patientAvailable = _appointmentRepository.IsPatientAvailableForRescheduel(
            appointment.PatientID, newDateTime, appointment.AppointmentID);
        if (!doctorAvailable || !patientAvailable)
            return false; // Doctor or patient is not available at the new date/time

        appointment.AppointmentDateTime = newDateTime;
        appointment.AppointmentStatus = enAppointmentStatus.Rescheduled;
        return _appointmentRepository.Update(appointment);
    }

    /// <summary>
    /// Schedules a new appointment for a doctor and patient at a specified date and time.
    /// </summary>
    /// <param name="doctor">The doctor for the appointment.</param>
    /// <param name="patient">The patient for the appointment.</param>
    /// <param name="date">The date and time of the appointment.</param>
    /// <returns>The ID of the scheduled appointment, or -1 if scheduling failed.</returns>
    public int ScheduleAppointment(Doctor doctor, Patient patient, DateTime date)
    {
        if (_appointmentRepository.IsDoctorAvailable(doctor.DoctorID, date) && _appointmentRepository.IsPatientAvailable(patient.PatientID, date))
        {
            Appointments appointment = new Appointments
            {
                DoctorID = doctor.DoctorID,
                PatientID = patient.PatientID,
                AppointmentDateTime = date,
                AppointmentStatus = enAppointmentStatus.Scheduled
            };
            _appointmentRepository.Add(appointment);
            return appointment.AppointmentID;
        }
        return -1;
    }

    /// <summary>
    /// Cancels an existing appointment if it is scheduled or rescheduled and not in the past.
    /// </summary>
    /// <param name="appointment">The appointment to cancel.</param>
    /// <returns>True if the appointment was successfully canceled; otherwise, false.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the appointment is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the appointment cannot be canceled.</exception>
    /// <exception cref="Exception">Thrown if the appointment does not exist.</exception>
    public bool CancelAppointment(Appointments appointment)
    {
        if (appointment == null)
        {
            throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null");
        }
        if (appointment.AppointmentStatus != enAppointmentStatus.Scheduled && appointment.AppointmentStatus != enAppointmentStatus.Rescheduled)
        {
            throw new InvalidOperationException("Only scheduled or rescheduled appointments can be cancelled.");
        }
        if (appointment.AppointmentDateTime < DateTime.Now)
        {
            throw new InvalidOperationException("Cannot cancel an appointment in the past.");
        }
        if (_appointmentRepository.DoesExist(appointment.AppointmentID) == false)
        {
            throw new Exception("Appointment does not exist");
        }
        else
        {
            return _appointmentRepository.CancelAppointment(appointment);
        }
    }

    /// <summary>
    /// Retrieves an appointment by its unique identifier.
    /// </summary>
    /// <param name="appointmentId">The appointment ID.</param>
    /// <returns>The appointment with the specified ID.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the appointment ID is negative.</exception>
    /// <exception cref="KeyNotFoundException">Thrown if the appointment does not exist.</exception>
    public Appointments GetAppointmentById(int appointmentId)
    {
        if (appointmentId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(appointmentId), "cannot be negative");
        }

        var appointment = _appointmentRepository.GetByID(appointmentId);

        if (appointment == null)
            throw new KeyNotFoundException("Appointment does not exist.");

        return appointment;
    }

    /// <summary>
    /// Retrieves all appointments for a specific doctor.
    /// </summary>
    /// <param name="doctorId">The doctor's ID.</param>
    /// <returns>An <see cref="IQueryable{Appointments}"/> of appointments for the doctor.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the doctor ID is negative.</exception>
    /// <exception cref="Exception">Thrown if no appointments are found for the doctor.</exception>
    public IQueryable<Appointments> GetAppointmentsByDoctorId(int doctorId)
    {
        if (doctorId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(doctorId), " cannot be negative");
        }
        var appointments = _appointmentRepository.GetAppointmentsByDoctorId(doctorId);
        if (appointments == null)
            throw new Exception("No appointments found for this doctor.");
        return appointments;
    }

    /// <summary>
    /// Retrieves all appointments for a specific patient.
    /// </summary>
    /// <param name="patientId">The patient's ID.</param>
    /// <returns>An <see cref="IQueryable{Appointments}"/> of appointments for the patient.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Thrown if the patient ID is negative.</exception>
    /// <exception cref="Exception">Thrown if no appointments are found for the patient.</exception>
    public IQueryable<Appointments> GetAppointmentsByPatientId(int patientId)
    {
        if (patientId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(patientId), " cannot be negative");
        }
        var appointments = _appointmentRepository.GetAppointmentsByPatientId(patientId);
        if (appointments == null)
            throw new Exception("No appointments found for this patient.");
        return appointments;
    }
}

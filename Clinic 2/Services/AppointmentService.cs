using DataLayer.Repositories.Interfaces;
public class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;

    public AppointmentService(IAppointmentRepository appointmentRepo)
    {
        _appointmentRepository = appointmentRepo;
    }

    public bool RescheduleAppointment(Appointments appointment, DateTime newDateTime)
    {
        if (!_appointmentRepository.IsDoctorAvailableForRescheduel(appointment.DoctorID, newDateTime, appointment.AppointmentID) ||
            !_appointmentRepository.IsPatientAvailableForRescheduel(appointment.PatientID, newDateTime, appointment.AppointmentID))
        {
            return false; // Doctor or patient is not available at the new date/time
        }
        else
        {
            appointment.AppointmentDateTime = newDateTime;
            appointment.AppointmentStatus = enAppointmentStatus.Rescheduled;
            return _appointmentRepository.Update(appointment);
         
        }

    }

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

    bool IAppointmentService.CancelAppointment(Appointments appointment)
    {
        if (appointment == null)
        {
            throw new ArgumentNullException(nameof(appointment), "Appointment cannot be null");
        }
        if (appointment.AppointmentStatus == enAppointmentStatus.Scheduled&& appointment.AppointmentStatus!=enAppointmentStatus.Rescheduled)
        {
            throw new InvalidOperationException("Only scheduled or rescheduled appointments can be cancelled.");
        }
        if (appointment.AppointmentDateTime < DateTime.Now)
        {
            throw new InvalidOperationException("Cannot cancel an appointment in the past.");
        }
        if (!_appointmentRepository.DoesExist(appointment.AppointmentID))
        {
            throw new Exception("Appointment does not exist");
        }
        else
        {
            return _appointmentRepository.CancelAppointment(appointment);
            
        }
    }

    public Appointments GetAppointmentById(int appointmentId)
    {
        if (appointmentId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(appointmentId), "cannot be negative");
        }

        var appointment = _appointmentRepository.GetByID(appointmentId);

        if (appointment == null)
            throw new Exception("Appointment does not exist.");

        return appointment;
    }

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

    public IQueryable<Appointments> GetAppointmentsByPatientId(int patientId)
    {
        if (patientId < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(patientId), " cannot be negative");
        }
        var appointments = _appointmentRepository.GetAppointmentsByDoctorId(patientId);
        if (appointments == null)
            throw new Exception("No appointments found for this patient.");
        return appointments;
    }

}

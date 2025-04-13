using DataLayer.Data;
using DataLayer.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            appointment.AppointmentStatus = AppointmentStatus.Rescheduled;
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
                AppointmentStatus = AppointmentStatus.Scheduled
            };
            _appointmentRepository.Add(appointment);
            return appointment.AppointmentID;
        }
        return -1;
    }
}

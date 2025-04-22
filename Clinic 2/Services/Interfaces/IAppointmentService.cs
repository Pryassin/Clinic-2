public interface IAppointmentService
{
    bool RescheduleAppointment(Appointments appointment, DateTime newDateTime);
    int ScheduleAppointment(Doctor doctor, Patient patient, DateTime date);
    bool CancelAppointment(Appointments appointment);
    Appointments GetAppointmentById(int appointmentId);
    public IQueryable<Appointments> GetAppointmentsByPatientId(int patientId);
    public IQueryable<Appointments> GetAppointmentsByDoctorId(int doctorId);

}

namespace DataLayer.Repositories.Interfaces
{
    public interface IAppointmentRepository : IBaseRepository<Appointments>
    {
        public bool CancelAppointment(Appointments appointment);
        public IQueryable<Appointments> GetAppointmentsByPatientId(int patientId);
        public IQueryable<Appointments> GetAppointmentsByDoctorId(int doctorId);
        public bool IsDoctorAvailable(int doctorId, DateTime dateTime);
        public bool IsPatientAvailable(int patientId, DateTime dateTime);
        public bool IsPatientAvailableForRescheduel(int PatientID, DateTime dateTime, int AppointmentID);
        public bool IsDoctorAvailableForRescheduel(int DoctorID, DateTime dateTime, int AppointmentID);

    }
}

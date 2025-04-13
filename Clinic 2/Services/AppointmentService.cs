using DataLayer.Data;

public class AppointmentService : AppointmentRepository, IAppointmentService
{
    public AppointmentService(ClinicDbContext context) : base(context)
    {
    }

    public Appointments GetById(int ID)
    {
        throw new NotImplementedException();
    }
}

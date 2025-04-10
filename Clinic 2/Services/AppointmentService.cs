using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class AppointmentService : BaseRepository<Appointments>, IAppointment
{
    public AppointmentService(ClinicDbContext context) : base(context)
    {
    }

    public Appointments GetById(int ID)
    {
        throw new NotImplementedException();
    }
}
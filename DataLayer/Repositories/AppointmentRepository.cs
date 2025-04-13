using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class AppointmentRepository : BaseRepository<Appointments>, IAppointmentRepository
{
    public AppointmentRepository(ClinicDbContext context) : base(context)
    {
    }
}
using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PrescriptionsService :BaseRepository<Prescription>, IPrescriptions
{
    private readonly ClinicDbContext _context;
   public PrescriptionsService(ClinicDbContext context) : base(context)
    {
        _context = context;
    }

}


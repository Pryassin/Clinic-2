using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PatientService : BaseRepository<Patient> ,IPatientService
{
    private readonly ClinicDbContext _context;
    public PatientService(ClinicDbContext context):base(context)
    {
   
    }
}


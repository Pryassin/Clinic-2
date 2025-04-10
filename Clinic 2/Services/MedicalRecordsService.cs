using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class MedicalRecordsService : BaseRepository<MedicalRecord>, IMedicalRecords
{
    private readonly ClinicDbContext _context;
    public MedicalRecordsService(ClinicDbContext context) : base(context)
    {
        _context = context;
    }


}
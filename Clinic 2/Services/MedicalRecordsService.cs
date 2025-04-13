using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class MedicalRecordsService :MedicalRecordRepository, IMedicalRecordsService
{
    
    public MedicalRecordsService(ClinicDbContext context) : base(context)
    {
     
    }


}
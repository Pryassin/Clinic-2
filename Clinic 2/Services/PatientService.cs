using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PatientService : PatientRepository ,IPatientService
{
    
    public PatientService(ClinicDbContext context):base(context)
    {
   
    }
}


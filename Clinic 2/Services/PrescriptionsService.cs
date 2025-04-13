using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PrescriptionsService : PrescriptionRepository, IPrescriptions
{
    public PrescriptionsService(ClinicDbContext context) : base(context)
    {
       
    }

}


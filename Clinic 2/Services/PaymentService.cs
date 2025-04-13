using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PaymentService : PaymentRepository, IPaymentService
{
    public PaymentService(ClinicDbContext context) : base(context)
    {
    }
}


   



using DataLayer.Data;
using DataLayer.Repositories;
using DataLayer.Repositories.Interfaces;

public class PaymentService :IPaymentService
{
    IPatientRepository _patientRepository;  
    public PaymentService(IPatientRepository patientRepository)
    {
        _patientRepository = patientRepository;
    }
}


   


